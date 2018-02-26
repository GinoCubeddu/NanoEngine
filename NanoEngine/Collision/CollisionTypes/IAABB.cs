using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision.CollisionTypes
{
    public interface IAABB
    {
        /// <summary>
        /// Checks to see if 2 axis aligned bounding box's are
        /// coliding
        /// </summary>
        /// <param name="asset1">The first asset to be checked against</param>
        /// <param name="asset2">The second asset to be checked against</param>
        /// <returns>A boolean value telling us if there has been a collision</returns>
        bool CheckCollision(IAsset asset1, IAsset asset2);
    }
}
