using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Collision.CollisionTypes;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision.Manager
{
    public class CollisionManager : ICollisionManager
    {
        // SAT collision checker
        private ISAT _sat;

        // AABB collsion checker
        private IAABB _aabb;

        public CollisionManager()
        {
            _sat = new SAT();
            _aabb = new AABB();
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
            foreach (Tuple<IAsset, IAiComponent> possibleCollision in possibleCollisions)
            {
                bool collision = false;
                if (asset.Item1 is IAABBColidable && possibleCollision.Item1 is IAABBColidable)
                    collision = _aabb.CheckCollision(asset.Item1, possibleCollision.Item1);
                else
                    collision = _sat.CheckCollision(asset.Item1, possibleCollision.Item1);

                if (collision)
                {
                    (asset.Item2 as ICollisionResponder)?.CollisionResponse(
                        new CollisionResponse(possibleCollision.Item1)
                    );
                    (possibleCollision.Item2 as ICollisionResponder)?.CollisionResponse(
                        new CollisionResponse(asset.Item1)    
                    );
                }
            }
        }
    }
}
