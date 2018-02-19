using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.Collision;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;

namespace NanoEngine.ObjectManagement.Managers
{
    public class AssetManager : IAssetManager
    {
        private int _uid;

        private IDictionary<string, IAsset> _assetDictionary;

        private IDictionary<string, IAiComponent> _aiComponents;

        private IAssetFactory _assetFactory;

        private IAiFactory _aiFactory;

        private IQuadTree _quadTree;

        public AssetManager()
        {
            _uid = 0;
            _assetDictionary = new Dictionary<string, IAsset>();
            _aiComponents = new Dictionary<string, IAiComponent>();
            _assetFactory = new AssetFactory();
            _aiFactory = new AiFactory();
            _quadTree = new QuadTree(1, 5, new Rectangle(0, 0, 800, 1000));
            QuadTree.DrawQuadTrees = true;
        }

        /// <summary>
        /// Creates an asset along with it's mind and add's. Once created
        /// the Asset will be automaticly drawn and the Mind will automaticly
        /// be updated if spawn is set to true. Otherwise if spawn is false
        /// the Asset will not be drawn or updated.
        /// </summary>
        /// <typeparam name="T">The type of asset that you want</typeparam>
        /// <typeparam name="U">The type of mind you want</typeparam>
        /// <param name="uName">The name which will be given to the 
        /// asset</param>
        /// <param name="posX">The X position of the asset</param>
        /// <param name="PosY">The Y position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public void CreateAsset<T, U>(string uName, int posX, int PosY, bool spawn = true) 
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            CreateAsset<T, U>(uName, new Vector2(posX, PosY), spawn);
        }

        /// <summary>
        /// Creates an asset along with it's mind and add's. Once created
        /// the Asset will be automaticly drawn and the Mind will automaticly
        /// be updated if spawn is set to true. Otherwise if spawn is false
        /// the Asset will not be drawn or updated.
        /// </summary>
        /// <typeparam name="T">The type of asset that you want</typeparam>
        /// <typeparam name="U">The type of mind you want</typeparam>
        /// <param name="uName">The name which will be given to the 
        /// asset</param>
        /// <param name="pos">The position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public void CreateAsset<T, U>(string uName, Vector2 pos, bool spawn = true) 
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            try
            {
                _assetDictionary.Add(
                    uName,
                    _assetFactory.RetriveNewAsset<T>(
                        uName, pos
                    )
                );

                _aiComponents.Add(
                    uName,
                    _aiFactory.CreateAi<U>()
                );
                if (_aiComponents[uName] is IAssetmanagerNeeded)
                    (_aiComponents[uName] as IAssetmanagerNeeded).AssetManager = this;

                _aiComponents[uName].InitialiseAiComponent(_assetDictionary[uName]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Creates an asset along with it's mind and add's. Once created
        /// the Asset will be automaticly drawn and the Mind will automaticly
        /// be updated if spawn is set to true. Otherwise if spawn is false
        /// the Asset will not be drawn or updated.
        /// </summary>
        /// <typeparam name="T">The type of asset that you want</typeparam>
        /// <typeparam name="U">The type of mind you want</typeparam>

        /// <param name="posX">The X position of the asset</param>
        /// <param name="PosY">The Y position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public void CreateAsset<T, U>(int posX, int posY, bool spawn = true)
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            CreateAsset<T, U>(
                typeof(T).ToString() + _uid.ToString(),
                new Vector2(posX, posY),
                spawn
            );
            _uid++;
        }

        /// <summary>
        /// Creates an asset along with it's mind and add's. Once created
        /// the Asset will be automaticly drawn and the Mind will automaticly
        /// be updated if spawn is set to true. Otherwise if spawn is false
        /// the Asset will not be drawn or updated.
        /// </summary>
        /// <typeparam name="T">The type of asset that you want</typeparam>
        /// <typeparam name="U">The type of mind you want</typeparam>
        /// <param name="pos">The position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public void CreateAsset<T, U>(Vector2 pos, bool spawn = true)
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            CreateAsset<T, U>(
                typeof(T).ToString() + _uid.ToString(),
                pos,
                spawn
            );
            _uid++;
        }

        /// <summary>
        /// Draws all the assets on the screen if they are "spawned" in
        /// </summary>
        public void DrawAssets(IRenderManager rendermanager)
        {
            _quadTree.Clear();
            foreach (IAsset asset in _assetDictionary.Values)
            {
                _quadTree.Insert(asset);
                asset.Draw(rendermanager);
                rendermanager.Draw(rendermanager.BlankTexture, asset.Bounds, Color.White);
            }
            _quadTree.Draw(rendermanager);           
        }

        /// <summary>
        /// Updates all the minds of the assets if they are "spawned" in
        /// </summary>
        public void UpdateAssets()
        {
            foreach (IAiComponent item in _aiComponents.Values)
            {
                item.Update();
            }
        }

        /// <summary>
        /// Deletes the asset and it's mind
        /// </summary>
        /// <param name="uName">The ID of the asset</param>
        public void DeleteAsset(string uName)
        {
            // Only delete if asset is found
            if (!_assetDictionary.ContainsKey(uName))
                throw new KeyNotFoundException(
                    "No asset by the name of " + uName + " exsists"
                );

            // If the asset has a mind delete that first
            if (_aiComponents.ContainsKey(uName))
            {
                _aiComponents[uName] = null;
                _aiComponents.Remove(uName);
            }                
            
            // Finaly delete the asset
            _assetDictionary[uName] = null;
            _assetDictionary.Remove(uName);          
        }

        /// <summary>
        /// Returns the requested asset
        /// </summary>
        /// <param name="uName">the ID of the asset</param>
        /// <returns>The asset with the requested ID</returns>
        public IAsset RetriveAsset(string uName)
        {
            // Checks to see if the key exsists before returning
            if (_assetDictionary.ContainsKey(uName))
                return _assetDictionary[uName];

            // Throw error if asset was not found
            throw new KeyNotFoundException(
                "No asset by the name of " + uName + " exsists"
            );
        }

        /// <summary>
        /// Returns the requested AICompnent
        /// </summary>
        /// <param name="uName">the ID of the AI</param>
        /// <returns>An AI that belongs to the requested asset</returns>
        public IAiComponent RetriveAssetAI(string uName)
        {
            if (_aiComponents.ContainsKey(uName))
                return _aiComponents[uName];

            // Throw error if asset was not found
            throw new KeyNotFoundException("No ai by the name of " + uName + " exsists");
        }

        /// <summary>
        /// Unloads and destroys all created assets and their minds
        /// </summary>
        public void UnloadAssets()
        {
            // Unload all assets within _aiComponents
            foreach (string key in _aiComponents.Keys)            
                _aiComponents[key] = null;

            // Unload all assets within _assetDictonary
            foreach (string key in _assetDictionary.Keys)
                _assetDictionary[key] = null;

            // Remove all items from both dictonaries
            _aiComponents.Clear();
            _assetDictionary.Clear();            
        }
    }
}
