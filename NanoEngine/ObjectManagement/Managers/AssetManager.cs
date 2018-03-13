using System;
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
using OpenTK.Graphics.ES20;

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

        // An instance of the physics manager
        private IPhysicsManager _physicsManager;

        // A bool informing us if we need to draw the bounds or not
        public static bool DrawBounds = false;


        public AssetManager(IEventManager eventManager)
        {
            _uid = 0;
            _assetDictionary = new Dictionary<string, IAsset>();
            _aiComponents = new Dictionary<string, IAiComponent>();
            _assetFactory = new AssetFactory();
            _aiFactory = new AiFactory(eventManager);
            _collisionManager = new CollisionManager();
            _physicsManager = new PhysicsManager();
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
        public void CreateAsset<T, U>(string uName, int posX, int posY, bool spawn = true) 
            where T : IAsset, new()
            where U : IAiComponent, new()
        {
            CreateAsset<T, U>(uName, new Vector2(posX, posY), spawn);
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
        public void CreateAsset<T>(string uName, int posX, int posY, bool spawn = true) where T : IAsset, new()
        {
            CreateAsset<T>(uName, new Vector2(posX, posY), spawn);
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
                // offload the creation to the asset factory
                _assetDictionary.Add(
                    uName,
                    _assetFactory.RetriveNewAsset<T>(
                        uName, pos
                    )
                );

                // off load the creation to the ai factory
                _aiComponents.Add(
                    uName,
                    _aiFactory.CreateAi<U>()
                );

                // THINK ABOUT HOW THIS CAN BE REFACTORED - SHOULD NOT BE HERE
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
        /// <param name="uName">The name which will be given to the 
        /// asset</param>
        /// <param name="pos">The position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public void CreateAsset<T>(string uName, Vector2 pos, bool spawn = true) where T : IAsset, new()
        {
            try
            {
                // offload the creation of the asset to the asset factory
                _assetDictionary.Add(
                    uName,
                    _assetFactory.RetriveNewAsset<T>(
                        uName, pos
                    )
                );
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
        /// <param name="posX">The X position of the asset</param>
        /// <param name="posY">The Y position of the asset</param>
        /// <param name="spawn">Deciding if we want the asset to be
        /// spawned straight away</param>
        public void CreateAsset<T>(int posX, int posY, bool spawn = true) where T : IAsset, new()
        {
            CreateAsset<T>(
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
        public void CreateAsset<T>(Vector2 pos, bool spawn = true) where T : IAsset, new()
        {
            CreateAsset<T>(
                typeof(T).ToString() + _uid.ToString(),
                pos,
                spawn
            );
            _uid++;
        }

        /// <summary>
        /// Load all the assets from a json file. All ids of all assets within the
        /// json file MUST be added through the static methods of the LevelLoader
        /// class first
        /// </summary>
        /// <param name="filename">The name of the json file within the Content directory</param>
        public void LoadLevel(string filename)
        {
            // THIS IS BREAKING SINGLE RESPONSIBILTY NEEDS REFACTORING
            ILevelLoader loader = new LevelLoader();
            _uid = loader.LoadTileMap(filename, _assetDictionary, _aiComponents, _assetFactory, _aiFactory, _uid);
            _collisionManager = new CollisionManager(loader.LevelBounds);

            foreach (IAiComponent aiComponent in _aiComponents.Values)
            {
                if (aiComponent is IAssetmanagerNeeded)
                    (aiComponent as IAssetmanagerNeeded).AssetManager = this;
                aiComponent.InitialiseAiComponent(aiComponent.ControledAsset);
            }
        }

        /// <summary>
        /// Draws all the assets on the screen if they are "spawned" in
        /// </summary>
        public void DrawAssets(IRenderManager rendermanager)
        {
            // loop through all assets and update them
            IList<IAsset> assets = _assetDictionary.Values.ToList();
            foreach (IAsset asset in assets)
                asset.Draw(rendermanager);
        }

        /// <summary>
        /// Updates all the minds of the assets if they are "spawned" in
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public void UpdateAssets(IUpdateManager updateManager)
        { 
            // update the physics manager
            _physicsManager.UpdatePhysics(_assetDictionary.Values.ToList());

            // Pass copies of the assets and their minds to the collision manager
            _collisionManager.Update(
                new Dictionary<string, IAsset>(_assetDictionary),
                new Dictionary<string, IAiComponent>(_aiComponents)
            );

            // loop through all minds and update them
            IList<IAiComponent> aiComponents = _aiComponents.Values.ToList();
            foreach (IAiComponent aiComponent in aiComponents)
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
