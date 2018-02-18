﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision
{
    public class QuadTree : IQuadTree
    {
        // Informs the quadrant of how many objects it should hold before splitting
        private int _maxObjects;

        // Informs the quad tree of how many levels it can split to
        private int _maxLevels;

        // Holds the level of the tree
        private int _level;

        // Holds the bounds for the current quadrant
        private Rectangle _bounds;

        // Holds the horizontal center point of the quad tree
        private int _horizontalCenterPoint;

        // Holds the vertical center point of the quad tree
        private int _verticalCenterPoint;

        // Holds the sub quads of the current quad tree level
        private IQuadTree[] _nodes;

        // Holds all the assets that belong to this level/quadrant
        private IList<IAsset> _assets;

        // Tells the quad tree wether to draw or not
        private bool _drawQuadTree;

        public QuadTree(int maxObjects, int maxLevels, Rectangle bounds)
            : this(0, maxObjects, maxLevels, bounds)
        {
            
        }

        protected QuadTree(int level, int maxObjects, int maxLevels, Rectangle bounds)
        {
            _level = level;
            _maxObjects = maxObjects;
            _maxLevels = maxLevels;
            _bounds = bounds;
            _verticalCenterPoint = _bounds.X + (_bounds.Width / 2);
            _horizontalCenterPoint = _bounds.Y + (_bounds.Height / 2);
            _nodes = new IQuadTree[4];
            _assets = new List<IAsset>();
            _drawQuadTree = false;
        }

        /// <summary>
        /// Method that clears all objects from the quadtree and its
        /// sub quads before setting its subquads to null. Should be
        /// called at the end of each update loop
        /// </summary>
        public void Clear()
        {
            // Delete the old list by overwriting it with a new one
            _assets = null;
            _assets = new List<IAsset>();

            // Loop through the sub quads and call the same method to clear
            // its assets then nullify the quadrant
            for (int i = 0; i < _nodes.Length; i++)
                if (_nodes[i] != null)
                {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
        }

        /// <summary>
        /// Method will draw the quadtree to the scene if the DrawQuadTree
        /// paramater is set to true. Defaults to false
        /// </summary>
        /// <param name="renderManager">The manager which will do the drawing</param>
        public void Draw(IRenderManager renderManager)
        {
            // Draws each side of the quad tree
            if (_drawQuadTree)
            {
                renderManager.Draw(
                    renderManager.BlankTexture,
                    new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, 3),
                    Color.White
                );
                renderManager.Draw(
                    renderManager.BlankTexture,
                    new Rectangle(_bounds.X, _bounds.Y + _bounds.Height, _bounds.Width, 3),
                    Color.White
                );
                renderManager.Draw(
                    renderManager.BlankTexture,
                    new Rectangle(_bounds.X + _bounds.Width, _bounds.Y, 3, _bounds.Height),
                    Color.White
                );
                renderManager.Draw(
                    renderManager.BlankTexture,
                    new Rectangle(_bounds.X, _bounds.Y, 3, _bounds.Height),
                    Color.White
                );

                // Call the same method on the sub trees
                if (_nodes[0] != null)
                    foreach (QuadTree quadTree in _nodes)
                        quadTree.Draw(renderManager);
            }
        }

        /// <summary>
        /// Method that inserts the passed in asset to the correct level
        /// and quadrant of the quad tree
        /// </summary>
        /// <param name="asset"></param>
        public void Insert(IAsset asset)
        {
            int index = GetAssetIndex(asset);
            // If the quadrant has sub quad trees we first want to attempt
            // to add it to one of those
            if (_nodes[0] != null && index != -1)
            {
                // Insert into the correct quad
                _nodes[index].Insert(asset);
                return;
            }
            else if (index == -1 && _nodes[0] != null)
            {
                // If the asset does not quite fit into one of the quadrants
                // AND the quadtree has already been split
                foreach (int i in GetQuadrantsIn(asset))
                    _nodes[i].Insert(asset);
            }

            // If we have reached hear it means that the quadtree has yet to split
            _assets.Add(asset);

            // check to see if the added asset causes the tree to "overflow"
            if (_assets.Count > _maxObjects && _level < _maxLevels)
            {
                // split the tree
                Split();

                // create a list to store the assets as we loop so we can remove
                // them later
                IList<IAsset> assetsToRemove = new List<IAsset>();
                // loop through the objects
                for (int i = 0; i < _assets.Count; i++)
                {
                    // get the index of the object
                    index = GetAssetIndex(_assets[i]);
                    // if the object fits into a sub quad
                    if (index != -1)
                        // insert into specific quadrant if it fully fits
                        _nodes[index].Insert(_assets[i]);
                    else
                        // insert into the multiple quadrants it is in
                        foreach (int j in GetQuadrantsIn(_assets[i]))
                            _nodes[j].Insert(_assets[i]);

                    // Add the asset for removal
                    assetsToRemove.Add(_assets[i]);
                }

                // Loop through the assets and remove them from this level
                foreach (IAsset asset1 in assetsToRemove)
                {
                    _assets.Remove(asset1);
                }
            }
        }

        /// <summary>
        /// Reteives all the possible assets that the passed in asset could
        /// collide with
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public IList<IAsset> RetriveCollidables(IAsset asset)
        {
            // Create list to store the collidables of this quadrant
            var possibleCollidables = new List<IAsset>();

            // Get the index of the asset we want to get the collidables for
            int index = GetAssetIndex(asset);

            // If the asset fits into the sub quadrants and subquadrants exsist
            if (index != -1 && _nodes[0] != null)
                // Call the retive collibale method on the sub quadrant that
                // the asset is in and add the returned list to the current
                possibleCollidables = possibleCollidables.Union(
                    _nodes[index].RetriveCollidables(asset)
                ).ToList();
            else if (index == -1 && _nodes[0] != null)
            {
                // Call the retive collibale method on each sub quadrant that
                // the asset is in and add the returned list to the current
                foreach (int i in GetQuadrantsIn(asset))
                    possibleCollidables = possibleCollidables.Union(
                        _nodes[i].RetriveCollidables(asset)
                    ).ToList();
            }

            // Add all the assets from this quadrant
            foreach (IAsset pAsset in _assets)
                if (pAsset.UniqueName != asset.UniqueName)
                    possibleCollidables.Add(pAsset);

            return possibleCollidables;
        }

        /// <summary>
        /// Method to split the quadtree into another quad tree by creating
        /// 4 sub quads as child nodes
        /// </summary>
        public void Split()
        {
            int subNodeWidth = (int)(_bounds.Width / 2);
            int subNodeHeight = (int)(_bounds.Height / 2);

            _nodes[0] = new QuadTree(
                _level + 1, _maxLevels, _maxObjects,
                new Rectangle(
                    _bounds.X, _bounds.Y, subNodeWidth, subNodeHeight
                )
            );
            _nodes[1] = new QuadTree(
                _level + 1, _maxLevels, _maxObjects,
                new Rectangle(
                    _bounds.X + subNodeWidth, _bounds.Y, subNodeWidth,
                    subNodeHeight
                )
            );
            _nodes[2] = new QuadTree(
                _level + 1, _maxLevels, _maxObjects,
                new Rectangle(
                    _bounds.X, _bounds.Y + subNodeHeight, subNodeWidth,
                    subNodeHeight
                )
            );
            _nodes[3] = new QuadTree(
                _level + 1, _maxLevels, _maxObjects,
                new Rectangle(
                    _bounds.X + subNodeWidth, _bounds.Y + subNodeHeight,
                    subNodeWidth, subNodeHeight
                )
            );
        }

        /// <summary>
        /// Toggles the DrawQuadTree variable on and off
        /// </summary>
        public void ToggleDraw()
        {
            // Invert the draw boolean
            _drawQuadTree = !_drawQuadTree;
            
            // If there are no nodes then we can return
            if (_nodes[0] == null) return;

            // Call toggle draw on all sub quadrants
            foreach (IQuadTree node in _nodes)
                node.ToggleDraw();
        }

        /// <summary>
        /// Gets the assets quadrant location within the quad tree and returns
        /// its index. A response of -1 means that the asset does not fit into
        /// any quadrant
        /// </summary>
        /// <param name="asset">The asset we are checking for</param>
        /// <returns>The index of the quadrant within the tree</returns>
        private int GetAssetIndex(IAsset asset)
        {
            // We want to return -1 if the asset does not fit into any of the
            // quadrants
            int index = -1;

            // boolean values to check if the asset is fully inside any of the quadrants
            bool topNode = (asset.Bounds.Y < _horizontalCenterPoint &&
                            asset.Bounds.Y + asset.Bounds.Height < _horizontalCenterPoint);

            bool botNode = (asset.Bounds.Y > _horizontalCenterPoint &&
                            asset.Bounds.Y + asset.Bounds.Height < _bounds.Y + _bounds.Height);

            bool leftNode = (asset.Bounds.X < _verticalCenterPoint &&
                             asset.Bounds.X + asset.Bounds.Width < _verticalCenterPoint);

            bool rightNode = (asset.Bounds.X > _verticalCenterPoint &&
                              asset.Bounds.X + asset.Bounds.Width < _bounds.X + _bounds.Width);

            // Check to see it the asset fits into any of the quadrants
            if (topNode && leftNode)
                index = 0;
            else if (topNode && rightNode)
                index = 1;
            else if (botNode && leftNode)
                index = 2;
            else if (botNode && rightNode)
                index = 3;

            return index;
        }

        /// <summary>
        /// Gets the index of all the quadrants that the passed in asset is in
        /// </summary>
        /// <param name="asset">The asset to check against</param>
        /// <returns></returns>
        private IList<int> GetQuadrantsIn(IAsset asset)
        {
            // Create an empty list
            IList<int> withinQuadrants = new List<int>();

            // Boolean values which will tell us if the passed in asset
            // is at all within any of the quadrants
            bool withinTop = (asset.Bounds.Y < _horizontalCenterPoint);

            bool withinBot = (asset.Bounds.Y + asset.Bounds.Height > _horizontalCenterPoint);

            bool withinLeft = (asset.Bounds.X < _verticalCenterPoint);

            bool withinRight = (asset.Bounds.X + asset.Bounds.Width > _verticalCenterPoint);

            // Use the combination of the checks to add the correct quadrant indexs
            // to the list before returning them.
            if (withinTop && withinLeft)
                withinQuadrants.Add(0);
            if (withinTop && withinRight)
                withinQuadrants.Add(1);
            if (withinBot && withinLeft)
                withinQuadrants.Add(2);
            if (withinBot && withinRight)
                withinQuadrants.Add(3);

            return withinQuadrants;

        }
    }
}
