using System;
using System.Collections.Generic;
using NanoEngine.Events.Args;
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

        /// <summary>
        /// checks matching types/interfaces on assets that are passed in
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="asset2"></param>
        void ProcessPhysics(IAsset asset, IAsset asset2, Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs> eventArgs);
    }
}
