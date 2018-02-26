using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision
{
    public interface IQuadTree
    {
        /// <summary>
        /// Method that clears all objects from the quadtree and its
        /// sub quads before setting its subquads to null. Should be
        /// called at the end of each update loop
        /// </summary>
        void Clear();

        /// <summary>
        /// Method will draw the quadtree to the scene if the DrawQuadTree
        /// paramater is set to true. Defaults to false
        /// </summary>
        /// <param name="renderManager">The manager which will do the drawing</param>
        void Draw(IRenderManager renderManager);

        /// <summary>
        /// Method that inserts the passed in asset to the correct level
        /// and quadrant of the quad tree
        /// </summary>
        /// <param name="asset"></param>
        void Insert(IAsset asset);

        /// <summary>
        /// Reteives all the possible assets that the passed in asset could
        /// collide with
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        IList<IAsset> RetriveCollidables(IAsset asset);

        /// <summary>
        /// Method to split the quadtree into another quad tree by creating
        /// 4 sub quads as child nodes
        /// </summary>
        void Split();
    }
}
