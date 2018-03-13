using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision.Manager
{
    public interface ICollisionManager
    {
        /// <summary>
        /// Checks to see if there is a collsion between an asset and a list
        /// of assets
        /// </summary>
        /// <param name="asset">A tuple of an asset and its mind</param>
        /// <param name="possibleCollisions">A list containingt tuples of assets and their minds</param>
        void CheckCollision(
            Tuple<IAsset, IAiComponent> asset,
            IList<Tuple<IAsset, IAiComponent>> possibleCollisions
        );

        /// <summary>
        /// Updates the collision manager against the passed in objects
        /// </summary>
        /// <param name="objects">
        /// A dictonary containing all currently active objects and their minds if they have one
        /// </param>
        void Update(IDictionary<string, Tuple<IAsset, IAiComponent>> objects);
    }
}
