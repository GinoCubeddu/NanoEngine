using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public bool CheckCollision(IAsset asset1, IAsset asset2)
        {
            Console.WriteLine("CHECKING THROUGH AABB");
            // IF left side of object one is LESS THAN right side of object two AND
            // IF right side of object one is GREATER than left side of object two AND
            // IF top side of object one is LESS THAN bottom side of object two AND
            // IF bootm side of object one is GREATER thane top side of object tow AND
            if (asset1.Bounds.X < asset2.Bounds.X + asset2.Bounds.Width &&
                asset1.Bounds.X + asset1.Bounds.Width > asset2.Bounds.X &&
                asset1.Bounds.Y < asset2.Bounds.Y + asset2.Bounds.Height &&
                asset1.Bounds.Y + asset1.Bounds.Height > asset2.Bounds.Y)
                return true;
            return false;
        }
    }
}
