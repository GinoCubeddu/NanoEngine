using System.Collections.Generic;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Physics
{
    internal interface IPhysicsManager
    {
        /// <summary>
        /// Updates the physics on all the assets that are passed in
        /// </summary>
        /// <param name="assets">A list of assets to update phsyics on</param>
        void UpdatePhysics(IList<IAsset> assets);
    }
}
