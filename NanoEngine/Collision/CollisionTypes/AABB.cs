using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision.CollisionTypes
{
    public class AABB : IAABB
    {
        /// <summary>
        /// Checks to see if 2 axis aligned bounding box's are
        /// coliding
        /// </summary>
        /// <param name="asset1">The first asset to be checked against</param>
        /// <param name="asset2">The second asset to be checked against</param>
        /// <returns>A boolean value telling us if there has been a collision</returns>
        public Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs> CheckCollision(IAsset asset1, IAsset asset2)
        {
            // IF left side of object one is LESS THAN right side of object two AND
            // IF right side of object one is GREATER than left side of object two AND
            // IF top side of object one is LESS THAN bottom side of object two AND
            // IF bootm side of object one is GREATER thane top side of object tow AND
            if (asset1.Bounds.X < asset2.Bounds.X + asset2.Bounds.Width &&
                asset1.Bounds.X + asset1.Bounds.Width > asset2.Bounds.X &&
                asset1.Bounds.Y < asset2.Bounds.Y + asset2.Bounds.Height &&
                asset1.Bounds.Y + asset1.Bounds.Height > asset2.Bounds.Y)
                return new Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs>(
                    GenerateCollisionEventArgs(asset1, asset2),
                    GenerateCollisionEventArgs(asset2, asset1)
                );
            return null;
        }

        private NanoCollisionEventArgs GenerateCollisionEventArgs(IAsset asset1, IAsset asset2)
        {
            Vector2 distance = Vector2.Zero;
            CollisionSide collisionSide;

            collisionSide = GetCollisionSide(asset1, asset2);


            // If asset 1 has collided with asset 2's top then the distance is
            // the Y + height of the first asset - the Y position of the second asset
            if (collisionSide == CollisionSide.TOP)
                distance = new Vector2(
                    0, -((asset2.Bounds.Y + asset2.Bounds.Height) - asset1.Bounds.Y)
                );
            // If asset 1 has collided with asset 2's bottom then the distance is
            // the Y + height of the second asset - the Y position of the first asset
            else if (collisionSide == CollisionSide.BOTTOM)
                distance = new Vector2(
                    0, (asset1.Bounds.Y + asset1.Bounds.Height) - asset2.Bounds.Y
                );
            // If asset 1 has collided with asset 2's top then the distance is
            // the Y + height of the first asset - the Y position of the second asset
            else if (collisionSide == CollisionSide.LEFT)
                distance = new Vector2(
                    -((asset2.Bounds.X + asset2.Bounds.Width) - asset1.Bounds.X), 0
                );
            // If asset 1 has collided with asset 2's top then the distance is
            // the Y + height of the first asset - the Y position of the second asset
            else if (collisionSide == CollisionSide.RIGHT)
                distance = new Vector2(
                    (asset1.Bounds.X + asset1.Bounds.Width) - asset2.Bounds.X, 0
                );
            // if we dont know the side then just push the entity to the top for now (THIS NEEDS CHANGING)
            else if (collisionSide == CollisionSide.UNKNOWN)
                distance = new Vector2(
                    (asset2.Bounds.X + asset2.Bounds.Width) - asset1.Bounds.X, 0
                );

            // Generate collision args
            return new NanoCollisionEventArgs()
            {
                CollidedWith = asset2,
                CollisionOverlap = distance,
                CollisionSide = collisionSide
            };
        }

        private CollisionSide GetCollisionSide(IAsset asset1, IAsset asset2)
        {
            float bc = (float)(asset2.Bounds.Y + asset2.Bounds.Height) - asset1.Bounds.Y;
            float tc = (float)(asset1.Bounds.Y + asset1.Bounds.Height) - asset2.Bounds.Y;
            float lc = (float)(asset1.Bounds.X + asset1.Bounds.Width) - asset2.Bounds.X;
            float rc = (float)(asset2.Bounds.X + asset2.Bounds.Width) - asset1.Bounds.X;

            if (asset1.Bounds.Height != asset1.Bounds.Width)
            {
                if (tc < bc && tc < lc && tc < rc)
                    return CollisionSide.TOP;
                if (bc < tc && bc < lc && bc < rc)
                    return CollisionSide.BOTTOM;
                if (lc < bc && lc < tc && lc < rc)
                    return CollisionSide.LEFT;
                if (rc < bc && rc < lc && rc < tc)
                    return CollisionSide.RIGHT;
            }
            else
            {
                if (tc > bc && tc > lc && tc > rc)
                    return CollisionSide.TOP;
                if (bc > tc && bc > lc && bc > rc)
                    return CollisionSide.BOTTOM;
                if (lc > bc && lc > tc && lc > rc)
                    return CollisionSide.LEFT;
                if (rc > bc && rc > lc && rc > tc)
                    return CollisionSide.RIGHT;
            }

            
            return CollisionSide.UNKNOWN;
        }
    }
}
