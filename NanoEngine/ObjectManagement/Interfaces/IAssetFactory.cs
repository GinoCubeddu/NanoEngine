using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace NanoEngine.ObjectManagement.Interfaces
{
    public interface IAssetFactory
    {
        /// <summary>
        /// Returns a new asset of type T
        /// </summary>
        /// <typeparam name="T">Type of asset</typeparam>
        /// <param name="uName">The name to give the asset</param>
        /// <param name="position">Vector2 position of the asset</param>
        /// <returns>a newly created asset</returns>
        IAsset RetriveNewAsset<T>(string uName, Vector2 position)
            where T : IAsset, new();

        /// <summary>
        /// Returns a new asset of type assetType
        /// </summary>
        /// <param name="assetType">Type of asset</param>
        /// <param name="uName">The name to give the asset</param>
        /// <param name="position">Vector2 position of the asset</param>
        /// <returns>a newly created asset</returns>
        /// <returns></returns>
        IAsset RetriveNewAsset(Type assetType, string uName, Vector2 position);

    }
}
