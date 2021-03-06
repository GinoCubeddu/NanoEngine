﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Collision;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Collision.CollisionTypes;
using NanoEngine.Collision.Manager;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.Events.Interfaces;
using NanoEngine.Physics;

namespace NanoEngine.ObjectManagement.Managers
{
    public class AssetManager : IAssetManager
    {
        // The uid that will be assigned to the next object
        private int _uid;

        // A dict containing all the currently updating assets
        private IDictionary<string, IAsset> _assetDictionary;

        // A dict containing all the available assets that are up for recycling
        private IDictionary<string, IAsset> _availableAssets;

        // A dict containing all the currently updating ai components
        private IDictionary<string, IAiComponent> _aiComponents;

        // A dict containing all the available ai components that are up for recycling
        private IDictionary<string, IAiComponent> _availableAiComponents;

        // An instance of the asset factory
        private IAssetFactory _assetFactory;

        // An instance of the ai factory
        private IAiFactory _aiFactory;

        // An insatnce of the collisionManager
        private ICollisionManager _collisionManager;

        // A bool informing us if we need to draw the bounds or not
        public static bool DrawBounds = false;

        // render filter used to fileter out any assets not within its bounds
        private IRenderFilter _renderFilter;

        // An instance of the event manager
        private IEventManager _eventManager;

        public AssetManager(IEventManager eventManager)
        {
            _uid = 0;
            _eventManager = eventManager;
            _assetDictionary = new Dictionary<string, IAsset>();
            _availableAssets = new Dictionary<string, IAsset>();
            _aiComponents = new Dictionary<string, IAiComponent>();
            _availableAiComponents = new Dictionary<string, IAiComponent>();
            _assetFactory = new AssetFactory();
            _aiFactory = new AiFactory(eventManager);
            _collisionManager = new CollisionManager();
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
        /// <param name="posY">The Y position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public string CreateAsset<T, U>(string uName, int posX, int posY, bool spawn = true) 
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            return CreateAsset<T, U>(uName, new Vector2(posX, posY), spawn);
        }

        /// <summary>
        /// Creates an asset along with it's mind and add's. Once created
        /// the Asset will be automaticly drawn and the Mind will automaticly
        /// be updated if spawn is set to true. Otherwise if spawn is false
        /// the Asset will not be drawn or updated.
        /// </summary>
        /// <typeparam name="T">The type of asset that you want</typeparam>
        /// <param name="uName">The name which will be given to the 
        /// asset</param>
        /// <param name="posX">The X position of the asset</param>
        /// <param name="posY">The Y position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public string CreateAsset<T>(string uName, int posX, int posY, bool spawn = true) where T : IAsset, new()
        {
            return CreateAsset<T>(uName, new Vector2(posX, posY), spawn);
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
        public string CreateAsset<T, U>(string uName, Vector2 pos, bool spawn = true) 
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            try
            {
                // if the asset manager does not have any spare assets to recycle
                // offload the creation to the asset factory
                if (!RecycleAsset(typeof(T), uName, pos))
                    _assetDictionary.Add(
                        uName,
                        _assetFactory.RetriveNewAsset<T>(
                            uName, pos
                        )
                    );

                // if the asset manager does not have any spare aiComponents to recycle
                // offload the creation to the ai factory
                if (!RecycleAi(typeof(U), _assetDictionary[uName]))
                    _aiComponents.Add(
                        uName,
                        _aiFactory.CreateAi<U>()
                    );

                // THINK ABOUT HOW THIS CAN BE REFACTORED - SHOULD NOT BE HERE
                if (_aiComponents[uName] is IAssetmanagerNeeded)
                    (_aiComponents[uName] as IAssetmanagerNeeded).AssetManager = this;

                _aiComponents[uName].InitialiseAiComponent(_assetDictionary[uName]);
                return uName;
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
        /// <param name="uName">The name which will be given to the 
        /// asset</param>
        /// <param name="pos">The position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public string CreateAsset<T>(string uName, Vector2 pos, bool spawn = true) where T : IAsset, new()
        {
            try
            {
                // if the asset manager does not have any spare assets to recycle
                // offload the creation to the asset factory
                if (!RecycleAsset(typeof(T), uName, pos))
                    _assetDictionary.Add(
                        uName,
                        _assetFactory.RetriveNewAsset<T>(
                            uName, pos
                        )
                    );
                return uName;
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
        /// <param name="posY">The Y position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public string CreateAsset<T, U>(int posX, int posY, bool spawn = true)
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            _uid++;
            return CreateAsset<T, U>(
                typeof(T).ToString() + _uid.ToString(),
                new Vector2(posX, posY),
                spawn
            );            
        }

        /// <summary>
        /// Creates an asset along with it's mind and add's. Once created
        /// the Asset will be automaticly drawn and the Mind will automaticly
        /// be updated if spawn is set to true. Otherwise if spawn is false
        /// the Asset will not be drawn or updated.
        /// </summary>
        /// <typeparam name="T">The type of asset that you want</typeparam>
        /// <param name="posX">The X position of the asset</param>
        /// <param name="posY">The Y position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public string CreateAsset<T>(int posX, int posY, bool spawn = true) where T : IAsset, new()
        {
            _uid++;
            return CreateAsset<T>(
                typeof(T).ToString() + _uid.ToString(),
                new Vector2(posX, posY),
                spawn
            );            
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
        public string CreateAsset<T, U>(Vector2 pos, bool spawn = true)
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            _uid++;
            return CreateAsset<T, U>(
                typeof(T).ToString() + _uid.ToString(),
                pos,
                spawn
            );                        
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
        public string CreateAsset<T>(Vector2 pos, bool spawn = true) where T : IAsset, new()
        {
            _uid++;
            return CreateAsset<T>(
                typeof(T).ToString() + _uid.ToString(),
                pos,
                spawn
            );            
        }

        /// <summary>
        /// Load all the assets from a json file. All ids of all assets within the
        /// json file MUST be added through the static methods of the LevelLoader
        /// class first
        /// </summary>
        /// <param name="filename">The name of the json file within the Content directory</param>
        public void LoadLevel(string filename)
        {
            // Create a new  level loader
            ILevelLoader loader = new LevelLoader();
            // Load the tile map and return the latest uid
            _uid = loader.LoadTileMap(filename, _assetDictionary, _aiComponents, _assetFactory, _aiFactory, _uid);

            // Recreate the asset manager with the new bounds
            _collisionManager = new CollisionManager(loader.LevelBounds);

            // Pass the asset manager to each ai component -- SHOULD NOT BE HERE
            foreach (IAiComponent aiComponent in _aiComponents.Values)
            {
                if (aiComponent is IAssetmanagerNeeded)
                    (aiComponent as IAssetmanagerNeeded).AssetManager = this;
                aiComponent.InitialiseAiComponent(aiComponent.ControledAsset);
            }
        }

        /// <summary>
        /// Draws all the assets on the scene if they are "spawned" in
        /// </summary>
        public void DrawAssets(IRenderManager rendermanager)
        {
            // Create a new asset list populated by the render filter or
            // the current asset list so we can loop through without worrying
            // about any assets being added/removed to the current list
            IList<IAsset> assets;
            if (_renderFilter != null)
                assets = _renderFilter.SortAssetsInRenderZone(_assetDictionary).Values.ToList();
            else
                assets = _assetDictionary.Values.ToList();

            // loop through all assets and draw them
            foreach (IAsset asset in assets)
                asset.Draw(rendermanager);
        }

        /// <summary>
        /// Updates all the minds of the assets if they are "spawned" in
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public void UpdateAssets(IUpdateManager updateManager)
        {
            // Create a dict for all the renderable assets and AI
            IDictionary<string, IAsset> renderableAssets = new Dictionary<string, IAsset>(_assetDictionary);
            IDictionary<string, IAiComponent> renderableAI = new Dictionary<string, IAiComponent>(_aiComponents);

            // If we have a render filter use it
            if (_renderFilter != null)
            {
                // Populate the asset dict from the render filter
                renderableAssets = _renderFilter.SortAssetsInRenderZone(new Dictionary<string, IAsset>(_assetDictionary));
                // Populate the ai dict from the render filter
                renderableAI = _renderFilter.SortAiInRenderZone(new Dictionary<string, IAiComponent>(_aiComponents));
            }

            // Pass copies of the assets and their minds to the collision manager
            _collisionManager.Update(
                renderableAssets,
                renderableAI
            );

            // Loop through the renderable assets and update them
            foreach (IAiComponent aiComponent in renderableAI.Values)
                aiComponent.Update(updateManager);

            CheckForDespawns();
        }

        /// <summary>
        /// Will check to see if there are any assets that need to be "despawned"
        /// </summary>
        private void CheckForDespawns()
        {
            // Loop check all assets to see if they want to be despawned
            foreach (string assetDictionaryKey in _assetDictionary.Keys.ToList())
            {
                // If it does want to be despawned
                if (_assetDictionary[assetDictionaryKey].Despawn)
                {
                    // Remove it from the current asset dict and add it to the available asset list
                    _availableAssets[assetDictionaryKey] = _assetDictionary[assetDictionaryKey];
                    _assetDictionary.Remove(assetDictionaryKey);

                    // If that asset had a ai component attached to it do the same
                    if (_aiComponents.ContainsKey(assetDictionaryKey))
                    {
                        // Remove all delegates from the event manager linked ot the mind
                        _eventManager.RemoveDelegates(_aiComponents[assetDictionaryKey]);

                        _availableAiComponents[assetDictionaryKey] = _aiComponents[assetDictionaryKey];
                        _aiComponents.Remove(assetDictionaryKey);
                    }
                }
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
                // Remove the develgaets for the ai
                _eventManager.RemoveDelegates(_aiComponents[uName]);
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

        public void SupplyRenderFilter(IRenderFilter renderFilter)
        {
            _renderFilter = renderFilter;
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

        /// <summary>
        /// Checks to see if there are any assets that we can recycle
        /// </summary>
        /// <param name="assetType">The asset type to recycle</param>
        /// <param name="uName">The new uNname for the asset</param>
        /// <param name="pos">The pos we want to give the asset</param>
        /// <returns>True if an asset was recycled otherwise false</returns>
        private bool RecycleAsset(Type assetType, string uName, Vector2 pos)
        {
            // If there are no available assets then return false
            if (_availableAssets.Count == 0)
                return false;

            // Get all the assets in the list
            IList<IAsset> avaliableAssets = _availableAssets.Values.ToList();

            // Loop through all available assets
            foreach (IAsset asset in avaliableAssets)
            {
                // if the asset is of the same type
                if (asset.GetType() == assetType)
                {
                    // Remove the asset from available assets
                    _availableAssets.Remove(asset.UniqueName);

                    // Reinit the asset with the new data
                    asset.Initilise();
                    asset.SetUniqueData(uName);
                    asset.SetPosition(pos);
                    asset.Despawn = false;

                    // Add the asset to the updating assets
                    _assetDictionary[uName] = asset;
                    return true;
                }   
            }
            return false;
        }

        /// <summary>
        /// Checks to see if there are any ai that we can recycle
        /// </summary>
        /// <param name="aiType">The type of the ai we want</param>
        /// <param name="asset">The asset that would be assigned to it</param>
        /// <returns>True if an aiComponent was recycled otherwise false</returns>
        private bool RecycleAi(Type aiType, IAsset asset)
        {
            // If there are no ai components to recycle then return false
            if (_availableAiComponents.Count == 0)
                return false;

            // Get all avaliable ai components
            IList<KeyValuePair<string, IAiComponent>> avaliableAI = _availableAiComponents.ToList();

            foreach (KeyValuePair<string, IAiComponent> aiComponent in avaliableAI)
            {
                // if the ai is of the correct type
                if (aiComponent.Value.GetType() == aiType)
                {
                    // remove the ai from the avalaible aicomponents
                    _availableAiComponents.Remove(aiComponent.Key);

                    // Reinit the ai with the asset
                    aiComponent.Value.InitialiseAiComponent(asset);

                    // Provide it to the event manager so it can re add the delegates if needed
                    _eventManager.AddDelegates(aiComponent);

                    // add the ai to the currently updating ai
                    _aiComponents[asset.UniqueName] = aiComponent.Value;
                    return true;
                }
            }
            // if we have reached here then there are no matching ai to recycle
            return false;
        }
    }
}
