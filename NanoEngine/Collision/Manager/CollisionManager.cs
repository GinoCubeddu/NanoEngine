using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Collision.CollisionTypes;
using NanoEngine.Core.Managers;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.Physics;

namespace NanoEngine.Collision.Manager
{
    public class CollisionManager : ICollisionManager
    {
        // SAT collision checker
        private ISAT _sat;

        // AABB collsion checker
        private IAABB _aabb;

        // Instance of the quad tree
        private IQuadTree _quadTree;

        private IPhysicsManager _physicsManager;


        /// <summary>
        /// Constructor for CollisionManager, uses dedault values for quad tree
        /// </summary>
        public CollisionManager() : this(5, 4, RenderManager.RenderBounds)
        {
            
        }

    

        /// <summary>
        /// Constructor for CollisionManager, uses dedault values for quad tree
        /// </summary>
        public CollisionManager(Rectangle levelBounds) : this(5, 4, levelBounds)
        {
        }

        /// <summary>
        /// Constructor for the CollisionManager
        /// </summary>
        /// <param name="quadTreeMaxObjects">The max objects the quad tree should contain</param>
        /// <param name="quadTreeMaxLevels">The max levels the the quad tree should have</param>
        /// <param name="quadTreeBounds">The bounds of the quadtree</param>
        public CollisionManager(int quadTreeMaxObjects, int quadTreeMaxLevels, Rectangle quadTreeBounds)
        {
            _sat = new SAT();
            _aabb = new AABB();
            _quadTree = new QuadTree(quadTreeMaxObjects, quadTreeMaxLevels, quadTreeBounds);
            _physicsManager = new PhysicsManager();
        }

        /// <summary>
        /// Checks to see if there is a collsion between an asset and a list
        /// of assets
        /// </summary>
        /// <param name="asset">A tuple of an asset and its mind</param>
        /// <param name="possibleCollisions">A list containingt tuples of assets and their minds</param>
        public void CheckCollision(
            Tuple<IAsset, IAiComponent> asset,
            IList<Tuple<IAsset, IAiComponent>> possibleCollisions
        )
        {
            // Loop through all possible collisions
            foreach (Tuple<IAsset, IAiComponent> possibleCollision in possibleCollisions)
            {
                // default the collision to null
                Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs> collision = null;

                // If both assets use aabb then we can check through AABB otherwise we NEED toc check
                // though SAT
                if (asset.Item1 is IAABBColidable && possibleCollision.Item1 is IAABBColidable)
                    collision = _aabb.CheckCollision(asset.Item1, possibleCollision.Item1);
                else
                {
                    // First do an AABB collision check to see if it is possible for a collision between
                    // the objects this is done so we don't waste rss on SAT and do a hiracahy of collision
                    //tests
                    if (_aabb.CheckCanCollide(asset.Item1, possibleCollision.Item1))
                    {
                        collision = _sat.CheckCollision(asset.Item1, possibleCollision.Item1);

                        // If there was a collision then set the collision side through aabb
                        if (collision != null)
                        {
                            collision.Item1.CollisionSide = _aabb.GetCollisionSide(asset.Item1, possibleCollision.Item1);
                            collision.Item2.CollisionSide = _aabb.GetCollisionSide(possibleCollision.Item1, asset.Item1);
                        }
                    }
                }

                // If the collision did not return null then send the collision responses
                if (collision != null)
                {
                    _physicsManager.ProcessPhysics(asset.Item1, possibleCollision.Item1, collision);

                    (asset.Item2 as ICollisionResponder)?.CollisionResponse(
                        collision.Item1
                    );
                    (possibleCollision.Item2 as ICollisionResponder)?.CollisionResponse(
                        collision.Item2
                    );
                }
            }
        }

        /// <summary>
        /// Checks to see if there is a collsion between an asset and a list
        /// of assets
        /// </summary>
        /// <param name="asset">A tuple of an asset and its mind</param>
        /// <param name="possibleCollisions">A list containingt tuples of assets and their minds</param>
        public void CheckCollisionInQuadrant(
            IList<Tuple<IAsset, IAiComponent>> possibleCollisions
        )
        {
            // Create a new dict to hold the names of everything that has been checked so we
            // dont run multiple of the same checks
            IDictionary<string, IList<string>> checkedCollisions = new Dictionary<string, IList<string>>();

            foreach (Tuple<IAsset, IAiComponent> possibleCollision in possibleCollisions)
            {
                checkedCollisions[possibleCollision.Item1.UniqueName] = new List<string>();
            }

            for (int i = 0; i < possibleCollisions.Count; i++)
            {
                for (int j = 0; j < possibleCollisions.Count; j++)
                {
                    // Continue if it is a self check
                    if (i == j)
                        continue;

                    // Last 2 checks of broad phase
                    // 1 - only do a check if at lease one object is "moveable"
                    // 2 - only do the check if it has not already been done
                    if ((!possibleCollisions[i].Item1.IsMovable && !possibleCollisions[j].Item1.IsMovable) ||
                        checkedCollisions[possibleCollisions[i].Item1.UniqueName]
                            .Contains(possibleCollisions[j].Item1.UniqueName))
                        continue;

                    // Inform asset to the asset it is about to be checked against
                    checkedCollisions[possibleCollisions[j].Item1.UniqueName].Add(possibleCollisions[i].Item1.UniqueName);


                    // default the collision to null
                    Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs> collision = null;

                    // If both assets use aabb then we can check through AABB otherwise we NEED toc check
                    // though SAT
                    if (possibleCollisions[i].Item1 is IAABBColidable && possibleCollisions[j].Item1 is IAABBColidable)
                        collision = _aabb.CheckCollision(possibleCollisions[i].Item1, possibleCollisions[j].Item1);
                    else
                    {
                        // First do an AABB collision check to see if it is possible for a collision between
                        // the objects this is done so we don't waste rss on SAT and do a hiracahy of collision
                        //tests
                        if (_aabb.CheckCanCollide(possibleCollisions[i].Item1, possibleCollisions[j].Item1))
                        {
                            collision = _sat.CheckCollision(possibleCollisions[i].Item1, possibleCollisions[j].Item1);

                            // If there was a collision then set the collision side through aabb
                            if (collision != null)
                            {
                                collision.Item1.CollisionSide = _aabb.GetCollisionSide(possibleCollisions[i].Item1, possibleCollisions[j].Item1);
                                collision.Item2.CollisionSide = _aabb.GetCollisionSide(possibleCollisions[i].Item1, possibleCollisions[j].Item1);
                            }
                        }
                    }

                    // If the collision did not return null then send the collision responses
                    if (collision != null)
                    {
                        (possibleCollisions[i].Item2 as ICollisionResponder)?.CollisionResponse(
                            collision.Item1
                        );
                        (possibleCollisions[j].Item2 as ICollisionResponder)?.CollisionResponse(
                            collision.Item2
                        );
                    }
                }

            }
        }

        /// <summary>
        /// Updates the collision manager against the passed in objects
        /// </summary>
        /// <param name="assets">All assets that are on screen</param>
        /// <param name="aiComponents">All AiComponents that belong to the assets</param>
        public void Update(IDictionary<string, IAsset> assets, IDictionary<string, IAiComponent> aiComponents)
        {
            _physicsManager.UpdatePhysics(assets.Values.ToList());

            // Remove all non collidables before continuing
            IDictionary<string, Tuple<IAsset, IAiComponent>> collidableAssets = GetCollidableAssets(assets, aiComponents);

            // Insert all assets into the quad tree
            InsertIntoQuadTree(collidableAssets);
            
            // Check for any collisions
            CheckForCollisions(collidableAssets);
        }

        /// <summary>
        /// Checks to see if any of the assets are colliding with eachother
        /// </summary>
        /// <param name="collidableAssets">All of the possible collidable assets</param>
        private void CheckForCollisions(IDictionary<string, Tuple<IAsset, IAiComponent>> collidableAssets)
        {
            // Get the collidables from each quadrant
            IList<IList<Tuple<IAsset, IAiComponent>>> collidableQuads = _quadTree.GetCollidablesByQuadrant();

            // Loop through the collidable lists the quadrants returned and check each list one at a time
            foreach (IList<Tuple<IAsset, IAiComponent>> collidableQuad in collidableQuads)
            {
                CheckCollisionInQuadrant(collidableQuad);
            }
        }

        /// <summary>
        /// Will insert all collidabel objects into the quad tree
        /// </summary>
        /// <param name="collidableAssets">All of the possible collidable assets</param>
        private void InsertIntoQuadTree(IDictionary<string, Tuple<IAsset, IAiComponent>> collidableAssets)
        {
            // We need to clear the quad tree before inserting anything
            _quadTree.Clear();
            // Loop through the passed in assets
            foreach (Tuple<IAsset, IAiComponent> assetData in collidableAssets.Values)
                _quadTree.Insert(assetData);
        }

        /// <summary>
        /// returns all collidbale assets and their minds in a dict
        /// </summary>
        /// <param name="assets">All assets that are on screen</param>
        /// <param name="aiComponents">All AiComponents that belong to the assets</param>
        /// <returns>The dict without the non collidables</returns>
        private IDictionary<string, Tuple<IAsset, IAiComponent>> GetCollidableAssets(
            IDictionary<string, IAsset> assets, IDictionary<string, IAiComponent> aiComponents   
        )
        {
            // Create a dict to hold the collidableAssets
            IDictionary<string, Tuple<IAsset, IAiComponent>> collidableAssets = new Dictionary<string, Tuple<IAsset, IAiComponent>>();

            // Loop through all the assets
            foreach (IAsset asset in assets.Values)
            {
                // If the asset is a collidable
                if (asset is ICollidable)
                {
                    // Check to see if the asset has an AI
                    IAiComponent ai = null;
                    if (aiComponents.ContainsKey(asset.UniqueName))
                        ai = aiComponents[asset.UniqueName];

                    // Add them both to the collidableAssets dict
                    collidableAssets[asset.UniqueName] = new Tuple<IAsset, IAiComponent>(
                        asset, ai
                    );
                }
            }
            // Retuen all collidbale assets
            return collidableAssets;
        }
    }
}
