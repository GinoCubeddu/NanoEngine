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
            if (CheckCanCollide(asset1, asset2))
                return new Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs>(
                    GenerateCollisionEventArgs(asset1, asset2),
                    GenerateCollisionEventArgs(asset2, asset1)
                );
            return null;
        }

        /// <summary>
        /// Checks to see if two axis aligned bounding boxes can collide
        /// </summary>
        /// <param name="asset1">The first asset to be checked against</param>
        /// <param name="asset2">The second asset to be checked against</param>
        /// <returns>A boolean value telling us if it is possible for a collision</returns>
        public bool CheckCanCollide(IAsset asset1, IAsset asset2)
        {
            if (asset1.Bounds.X < asset2.Bounds.X + asset2.Bounds.Width &&
                asset1.Bounds.X + asset1.Bounds.Width > asset2.Bounds.X &&
                asset1.Bounds.Y < asset2.Bounds.Y + asset2.Bounds.Height &&
                asset1.Bounds.Y + asset1.Bounds.Height > asset2.Bounds.Y)
                return true;
            return false;
        }

        private NanoCollisionEventArgs GenerateCollisionEventArgs(IAsset asset1, IAsset asset2)
        {
            // Set the distance and collision side to 0
            Vector2 distance = Vector2.Zero;
            CollisionSide collisionSide;

            // Get the correct collision side
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

        public CollisionSide GetCollisionSide(IAsset asset1, IAsset asset2)
        {
            // Get the values for which side asset 2 is on of asset 1
            float bc = (float)(asset2.Bounds.Y + asset2.Bounds.Height) - asset1.Bounds.Y; // bottom side of asset 1
            float tc = (float)(asset1.Bounds.Y + asset1.Bounds.Height) - asset2.Bounds.Y; //  top side of asset 1
            float lc = (float)(asset1.Bounds.X + asset1.Bounds.Width) - asset2.Bounds.X; // left side of asset 1
            float rc = (float)(asset2.Bounds.X + asset2.Bounds.Width) - asset1.Bounds.X; // right side of asset 1

            // If tc is bigger then eveything else it was a top collision
            if (tc > bc && tc > lc && tc > rc)
                return CollisionSide.TOP;

            // If bc is bigger then eveything else it was a bottom collision
            if (bc > tc && bc > lc && bc > rc)
                return CollisionSide.BOTTOM;

            // If lc is bigger then eveything else it was a left collision
            if (lc > bc && lc > tc && lc > rc)
                return CollisionSide.LEFT;

            // If rc is bigger then eveything else it was a right collision
            if (rc > bc && rc > lc && rc > tc)
                return CollisionSide.RIGHT;

            // If none match we dont know the collision side
            return CollisionSide.UNKNOWN;
        }
    }
}
