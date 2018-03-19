using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Physics
{
    internal class PhysicsManager : IPhysicsManager
    {
        /// <summary>
        /// Updates the physics on all the assets that are passed in
        /// </summary>
        /// <param name="assets">A list of assets to update phsyics on</param>
        public void UpdatePhysics(IList<IAsset> assets)
        {
            foreach (IAsset asset in assets)
            {
                if (!(asset is PhysicsEntity))
                    continue;
                PhysicsEntity pAsset = ((PhysicsEntity)asset);
                pAsset.Velocity *= pAsset.Damping;
                pAsset.Velocity += pAsset.Acceleration;
                pAsset.Position += pAsset.Velocity;
                pAsset.Acceleration = pAsset.Gravity;
            }
        }
    }
}
