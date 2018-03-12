﻿using System;
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

        // an instance of the quad tree
        private IQuadTree _quadTree;

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
            _quadTree = new QuadTree(2, 5, RenderManager.RenderBounds);
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
            _quadTree = new QuadTree(2, 5, loader.LevelBounds);

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
            // THIS IS BREAKING SINGLE RESPONSIBILITY NEEDS REFACTORING



           // SHOULD DO:
                // 1: loop through the assets and call their draw method - NOTHING ELSE




            // clear the current quad tree
            _quadTree.Clear();

            IList<string> assetKeys = _assetDictionary.Keys.ToList();
            foreach (string assetKey in assetKeys)
            {
                _assetDictionary[assetKey].Draw(rendermanager);
            }

            return;

            // loop through all the assets
            IList<string> assetKeys1 = _assetDictionary.Keys.ToList();
            foreach (string assetKey in assetKeys)
            {
                // Grab the asset
                IAsset asset = _assetDictionary[assetKey];

                if (asset.Despawn)
                    continue;

                if (asset is ICollidable)
                    _quadTree.Insert(asset);
                asset.Draw(rendermanager);

                if (DrawBounds)
                {
                    IList<Vector2> assetPoints = asset.Points ?? asset.GetPointsFromBounds();
                    for (int i = 0; i < assetPoints.Count; i++)
                    {
                        Vector2 edge = assetPoints[i + 1 == assetPoints.Count ? 0 : i + 1] - assetPoints[i];
                        float angle = (float)Math.Atan2(edge.Y, edge.X);
                        // draw lines between each point of object
                        rendermanager.Draw(
                            rendermanager.BlankTexture,
                            new Rectangle((int)assetPoints[i].X, (int)assetPoints[i].Y, (int)edge.Length(), 3),
                            null,
                            Color.Red,
                            angle,
                            new Vector2(0, 0),
                            SpriteEffects.None,
                            0
                        );
                    }
                }
            }

            foreach (string assetKey in assetKeys)
            {
                IAsset asset = _assetDictionary[assetKey];
                if (asset is ICollidable)
                {
                    if (asset.Despawn)
                        continue;

                    // If the asset is a colidable then we want to get all possible collidables
                    IList<IAsset> possibleCollidables = _quadTree.RetriveCollidables(asset);

                    // We dont want to check for collisions if there are none possible
                    if (possibleCollidables.Count == 0)
                        continue;
                    
                    // Set a null variable for the assetAI
                    IAiComponent assetAi = null;

                    // Create a new tuple list to hold the possible collidables
                    // and their minds
                    IList<Tuple<IAsset, IAiComponent>> possibleCollidablesList = new List<Tuple<IAsset, IAiComponent>>();

                    // Loop through each possible collsion and add them to the list
                    foreach (IAsset possibleCollidable in possibleCollidables)
                    {
                        // Reset the value to null and then attempt to get the AI for
                        // the current asset before adding it to the tuple list
                        assetAi = null;
                        _aiComponents.TryGetValue(possibleCollidable.UniqueName, out assetAi);
                        possibleCollidablesList.Add(new Tuple<IAsset, IAiComponent>(
                            possibleCollidable, assetAi    
                        ));
                    }

                    // Reset the ai to null before attempting to get the ai for the
                    // main asset
                    assetAi = null;
                    _aiComponents.TryGetValue(asset.UniqueName, out assetAi);
                    _collisionManager.CheckCollision(
                        new Tuple<IAsset, IAiComponent>(asset, assetAi),
                        possibleCollidablesList
                    );
                }
            }
            _quadTree.Draw(rendermanager);           
        }

        /// <summary>
        /// Updates all the minds of the assets if they are "spawned" in
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public void UpdateAssets(IUpdateManager updateManager)
        { 
            // THIS METHOD NEEDS TO CHANGE TO DO:
                // 1 - CALL COLLISION update (and pass the asset list - it is not the asset managers responisibilty to filiter out the non collidables)
                // 2 - MOVE the quad tree into the collision manager - it is not the responsibility of the asset manager to decide if an asset is
                    // a collidable and then insert it into the quad tree
                // 3 - DECIDE where the physics manager belongs...in this class at all?
                // 4 - loop through all minds and update them
                // 5 - call the CheckForDespawn method which will remove any entites that are marked for despawn from the dictonaires
                    // and add them  to the "available assets" dictonary


            // update the physics manager
            _physicsManager.UpdatePhysics(_assetDictionary.Values.ToList());

            // loop through all minds and update them
            IList<string> aiKeys = _aiComponents.Keys.ToList();
            foreach (string aiName in aiKeys)
            {
                if (!_aiComponents[aiName].ControledAsset.Despawn)
                    _aiComponents[aiName].Update(updateManager);
            }

        }

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

                    // If that asset had a 
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
