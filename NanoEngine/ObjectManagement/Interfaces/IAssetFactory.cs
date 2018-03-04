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
        /// Returns a new entity that can be used outside of the entity list
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="uName">The name to give the entity</param>
        /// <param name="position">Vector2 position of the asset</param>
        /// <returns>a newly created entity</returns>
        IAsset RetriveNewAsset<T>(string uName, Vector2 position)
            where T : IAsset, new();

        IAsset RetriveNewAsset(Type assetType, string uName, Vector2 position);

    }
}
