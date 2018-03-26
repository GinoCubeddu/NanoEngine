using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision.CollisionTypes
{
    public interface IAABB : ICollisionType
    {
        /// <summary>
        /// Checks to see if two axis aligned bounding boxes can collide
        /// </summary>
        /// <param name="asset1">The first asset to be checked against</param>
        /// <param name="asset2">The second asset to be checked against</param>
        /// <returns>A boolean value telling us if it is possible for a collision</returns>
        bool CheckCanCollide(IAsset asset1, IAsset asset2);
    }
}
