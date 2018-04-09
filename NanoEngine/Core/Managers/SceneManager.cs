using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.States;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Events;
using NanoEngine.Factories;
using NanoEngine.ObjectTypes.Control;

namespace NanoEngine.Core.Managers
{
    public class SceneManager : ISceneManager
    {
        // Holds the currently updating screens
        private IDictionary<string, IGameScreen> _updatingScreens;

        // Holds the screens that are avaialbe but not updating
        private IDictionary<string, IGameScreen> _avaliableScreens;

        // Holds all screens that are marked for deletion
        private IList<IGameScreen> _screensMarkedForDeletion;

        private ISceneFactory _sceneFactory;

        // Getter for the currently updating screens
        public IDictionary<string, IGameScreen> UpdatingScreens
        {
            get { return _updatingScreens; }
        }

        public SceneManager()
        {
            _updatingScreens = new Dictionary<string, IGameScreen>();
            _avaliableScreens = new Dictionary<string, IGameScreen>();
            _screensMarkedForDeletion = new List<IGameScreen>();
            _sceneFactory = new SceneFactory();
        }

        /// <summary>
        /// Adds a screen to the scene manager
        /// </summary>
        /// <typeparam name="T">The type of screen to add</typeparam>
        /// <param name="name">The id of the screen</param>
        public void AddScreen<T>(string name) where T : IGameScreen, new()
        {
            // Request a screen of the type T
            IGameScreen screen = _sceneFactory.CreateScreen<T>();

            if (_avaliableScreens.Count == 0 && _updatingScreens.Count == 0)
            {
                // If it is the first screen then create the vars and set it to updating
                _updatingScreens[name] = screen;
            } else
            {
                // If it is not the first screen then load it into the available screens
                _avaliableScreens[name] = screen;
            }
        }

        /// <summary>
        /// Tells the scene manager to start updating the screen
        /// </summary>
        /// <param name="name">The id of the screen to start updating</param>
        public void StartUpdatingScreen(string name)
        {
            // Only make the swap if the key 
            if (!_avaliableScreens.ContainsKey(name))
                return;

            // Add the screen from the avaliable screens to the updating screens
            _updatingScreens[name] = _avaliableScreens[name];
            _avaliableScreens.Remove(name);
        }

        /// <summary>
        /// Tells the scenemnager to stop updating a screen
        /// </summary>
        /// <param name="name">The id of the screen to stop updating</param>
        public void StopUpdatingScreen(string name)
        {
            // Only make the swap if the key is there
            if (!_updatingScreens.ContainsKey(name))
                return;

            // Add the screen from the updating screens to the avalible screens
            _avaliableScreens[name] = _updatingScreens[name];
            _updatingScreens.Remove(name);
        }

        /// <summary>
        /// Tells the scene manager to reload a screen
        /// </summary>
        /// <param name="name">The id of the screen to reload</param>
        public void ReloadScreen(string name)
        {
            // Set a variable to hold the screen
            IGameScreen screen;

            // Check to see if the screen is within any of the dictonaries
            if (_avaliableScreens.ContainsKey(name))
            {
                screen = _avaliableScreens[name];
            } else if (_updatingScreens.ContainsKey(name))
            {
                screen = _updatingScreens[name];
            } else
            {
                // There is no screen by that name
                return;
            }

            // Reload the screen
            screen.UnloadContent();
            screen.Initialise();
            screen.LoadContent();
        }

        /// <summary>
        /// Tells the scenemanager to delete a scene
        /// </summary>
        /// <param name="name">The id of the scene to delete</param>
        public void DeleteScreen(string name)
        {
            StopUpdatingScreen(name);
            // Only attempt to remove the screen 
            if (_avaliableScreens.ContainsKey(name))
            {
                // Mark the screen for deletion
                _screensMarkedForDeletion.Add(_avaliableScreens[name]);
                // remove from the avliable screens
                _avaliableScreens.Remove(name);
            } // If the screen was updating and not in avaliable screens
            else if (_updatingScreens.ContainsKey(name))
            {
                // mark screen for deletion
                _screensMarkedForDeletion.Add(_updatingScreens[name]);
                // Remove from the updating screens
                _updatingScreens.Remove(name);
            }
        }

        /// <summary>
        /// Checks to see if any screens have been marked for deletion and if so deletes them
        /// </summary>
        private void CheckForScreensForDeletion()
        {
            // No point continuing iif there are no items to delete
            if (_screensMarkedForDeletion.Count == 0)
                return;

            // Unload the content of each screen that is marked for deletion
            foreach (IGameScreen gameScreen in _screensMarkedForDeletion)
            {
                gameScreen.UnloadContent();
            }

            // Deletes the list by creating a new one
            _screensMarkedForDeletion = new List<IGameScreen>();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="updateManager">Provides a refrence to the updateManager.</param>
        public void Update(IUpdateManager updateManager)
        {
            // Check to see if any screens are waiting to be deleted
            CheckForScreensForDeletion();

            // Get a copy of the current scenes incase we need to edit the main one
            // mid loop
            IList<IGameScreen> screens = _updatingScreens.Values.ToList();

            // Call update on each scene
            foreach (IGameScreen screen in screens)
                screen.UpdateScreen(updateManager);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="renderManager">Provides a refrence to the renderManager.</param>
        public void Draw(IRenderManager renderManager)
        {
            // Call end draw first (part of quad tree draw hack)
            renderManager.EndDraw();

            // Get a copy of the current scenes incase we need to edit the main one
            // mid loop
            IList<IGameScreen> screens = _updatingScreens.Values.ToList();
            // Loop through each screen and call draw
            foreach (IGameScreen screen in screens)
            {
                // If we have a camera start the draw for the camera
                if (screen.Camera2D != null)
                {
                    renderManager.StartDraw(
                        SpriteSortMode.Deferred, BlendState.AlphaBlend, null,
                        null, null, null, screen.Camera2D.Transform
                    );
                }
                else
                {
                    // If we dont have camera start draw normaly
                    renderManager.StartDraw(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                }
                // Call Draw screen
                screen.DrawScreen(renderManager);
            }
        }
    }
}
