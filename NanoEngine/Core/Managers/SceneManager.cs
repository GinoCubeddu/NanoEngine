using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NanoEngine.Events;
using NanoEngine.Factories;
using NanoEngine.ObjectTypes.Control;

namespace NanoEngine.Core.Managers
{
    public class SceneManager : ISceneManager
    {
        // Holds the currently updating scenes
        private IOrderedDictionary _updatingScenes;

        // Holds the scenes that are avaialbe but not updating
        private IDictionary<string, IGameScene> _avaliableScenes;

        // Holds all scenes that are marked for deletion
        private IList<IGameScene> _scenesMarkedForDeletion;

        private ISceneFactory _sceneFactory;


        public SceneManager()
        {
            _updatingScenes = new OrderedDictionary();
            _avaliableScenes = new Dictionary<string, IGameScene>();
            _scenesMarkedForDeletion = new List<IGameScene>();
            _sceneFactory = new SceneFactory();
        }

        /// <summary>
        /// Adds a scene to the scene manager
        /// </summary>
        /// <typeparam name="T">The type of scene to add</typeparam>
        /// <param name="name">The id of the scene</param>
        public void AddScene<T>(string name) where T : IGameScene, new()
        {
            // Request a scene of the type T
            IGameScene scene = _sceneFactory.CreateScene<T>(name);

            if (_avaliableScenes.Count == 0 && _updatingScenes.Count == 0)
            {
                // If it is the first scene then create the vars and set it to updating
                _updatingScenes[name] = scene;
            } else
            {
                // If it is not the first scene then load it into the available scenes
                _avaliableScenes[name] = scene;
            }
        }

        /// <summary>
        /// Tells the scene manager to start updating the scene
        /// </summary>
        /// <param name="name">The id of the scene to start updating</param>
        public void StartUpdatingScene(string name)
        {
            // Only make the swap if the key 
            if (!_avaliableScenes.ContainsKey(name))
                return;

            // Add the scene from the avaliable scenes to the updating scenes
            _updatingScenes[name] = _avaliableScenes[name];
            _avaliableScenes.Remove(name);
        }

        /// <summary>
        /// Tells the scenemnager to stop updating a scene
        /// </summary>
        /// <param name="name">The id of the scene to stop updating</param>
        public void StopUpdatingScene(string name)
        {
            // Only make the swap if the key is there
            if (!_updatingScenes.Contains(name))
                return;

            // Add the scene from the updating scene to the avalible scenes
            _avaliableScenes[name] = (IGameScene)_updatingScenes[name];
            _updatingScenes.Remove(name);
        }

        /// <summary>
        /// Tells the scene manager to reload a scene
        /// </summary>
        /// <param name="name">The id of the scene to reload</param>
        public void ReloadScene(string name)
        {
            // Set a variable to hold the scene
            IGameScene scene;

            // Check to see if the scene is within any of the dictonaries
            if (_avaliableScenes.ContainsKey(name))
            {
                scene = _avaliableScenes[name];
            } else if (_updatingScenes.Contains(name))
            {
                scene = (IGameScene)_updatingScenes[name];
            } else
            {
                // There is no scene by that name
                return;
            }

            // Reload the scene
            scene.UnloadContent();
            scene.Initialise(scene.SceneName);
            scene.LoadContent();
        }

        /// <summary>
        /// Tells the scenemanager to delete a scene
        /// </summary>
        /// <param name="name">The id of the scene to delete</param>
        public void DeleteScene(string name)
        {
            StopUpdatingScene(name);
            // Only attempt to remove the scene 
            if (_avaliableScenes.ContainsKey(name))
            {
                // Mark the scene for deletion
                _scenesMarkedForDeletion.Add(_avaliableScenes[name]);
                // remove from the avliable scenes
                _avaliableScenes.Remove(name);
            } // If the scene was updating and not in avaliable scenes
            else if (_updatingScenes.Contains(name))
            {
                // mark scene for deletion
                _scenesMarkedForDeletion.Add((IGameScene)_updatingScenes[name]);
                // Remove from the updating scenes
                _updatingScenes.Remove(name);
            }
        }

        /// <summary>
        /// Checks to see if any scenes have been marked for deletion and if so deletes them
        /// </summary>
        private void CheckDeletedScenes()
        {
            // No point continuing if there are no items to delete
            if (_scenesMarkedForDeletion.Count == 0)
                return;

            // Unload the content of each scene that is marked for deletion
            foreach (IGameScene gameScene in _scenesMarkedForDeletion)
            {
                gameScene.UnloadContent();
            }

            // Deletes the list by creating a new one
            _scenesMarkedForDeletion = new List<IGameScene>();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="updateManager">Provides a refrence to the updateManager.</param>
        public void Update(IUpdateManager updateManager)
        {
            // Check to see if any scenes are waiting to be deleted
            CheckDeletedScenes();

            // Get a copy of the current scenes incase we need to edit the main one
            // mid loop
            IList<IGameScene> scenes = new List<IGameScene>();
            foreach (DictionaryEntry dictionaryEntry in _updatingScenes)
            {
                scenes.Add((IGameScene)dictionaryEntry.Value);
            }

            // Call update on each scene
            foreach (IGameScene scene in scenes)
                scene.UpdateScene(updateManager);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="renderManager">Provides a refrence to the renderManager.</param>
        public void Draw(IRenderManager renderManager)
        {
            // Get a copy of the current scenes incase we need to edit the main one
            // mid loop
            IList<IGameScene> scenes = new List<IGameScene>();
            foreach (DictionaryEntry dictionaryEntry in _updatingScenes)
            {
                scenes.Add((IGameScene)dictionaryEntry.Value);
            }
            // Loop through each scene and call draw
            foreach (IGameScene scene in scenes)
            {
                // Call end draw first (part of quad tree draw hack)
                renderManager.EndDraw();
                // If we have a camera start the draw for the camera
                if (scene.Camera2D != null)
                {
                    renderManager.StartDraw(
                        SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null,
                        null, null, null, scene.Camera2D.Transform
                    );
                }
                else
                {
                    // If we dont have camera start draw normaly
                    renderManager.StartDraw(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                }
                // Call Draw scene
                scene.DrawScene(renderManager);
            }
        }
    }
}
