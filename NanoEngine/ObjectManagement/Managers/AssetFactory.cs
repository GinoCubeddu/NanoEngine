﻿using Microsoft.Xna.Framework;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectManagement.Managers
{
    public class AssetFactory : IAssetFactory
    {
        /// <summary>
        /// Returns a new entity that can be used outside of the entity list
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="uName">The name to give the entity</param>
        /// <param name="position">Vector2 position of the asset</param>
        /// <returns>a newly created entity</returns>
        public IAsset RetriveNewAsset<T>(string uName, Vector2 position) where T : IAsset, new()
        {
            //Create new entity
            IAsset asset = new T();
            //set the start position
            InitiliseAsset(asset, uName, position);
            //Increase and set unquie id and name
            return asset;
        }

        public IAsset RetriveNewAsset(Type assetType, string uName, Vector2 position)
        {
            IAsset asset = (IAsset) Activator.CreateInstance(assetType);
            InitiliseAsset(asset, uName, position);
            return asset;
        }

        private void InitiliseAsset(IAsset asset, string uName, Vector2 position)
        {
            asset.SetPosition(position);

            asset.Initilise();

            asset.SetUniqueData(uName);
        }
    }
}
