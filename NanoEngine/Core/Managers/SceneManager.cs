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
using NanoEngine.ObjectTypes.Control;

namespace NanoEngine.Core.Managers
{
    public class SceneManager : ISceneManager
    {
        //Private static field holding the refrence to the manager
        private static ISceneManager manager;

        //Public Getter for scene manager making it a singleton
        public static ISceneManager Manager
        {
            get { return manager ?? (manager = new SceneManager()); }
        }

        //Public field for the screen dimentions
        private Vector2 screenDimentions;

        /// <summary>
        /// Public getter for the screen dimentions
        /// </summary>
        public Vector2 ScreenDimentions
        {
            get { return screenDimentions; }
        }

        //private fielf to hold the current screen
        private IGameScreen currentScreen;

        //private field for pause screen
        private IGameScreen pauseScreen;

        /// <summary>
        /// Private constructor so it can only be called in the class by the getter making it a singleton
        /// </summary>
        /// <param name="game">game of type Game</param>
        private SceneManager()
        {
        }

        /// <summary>
        /// Method to set the starting screen of the game
        /// </summary>
        /// <typeparam name="T">Type of screen to be created</typeparam>
        public void setStartScreen<T>() where T : IGameScreen, new()
        {
            //Create new screen
            currentScreen = new T();

            EventManager.Manager.AddDelegates(currentScreen);

            //load the screesn content
            LoadContent();
        }

        /// <summary>
        /// Method to set the pause screen of the game
        /// </summary>
        /// <typeparam name="T">The type of screen to be craeted</typeparam>
        public void setPauseScreen<T>() where T : IGameScreen, new()
        {
            //create new screen
            pauseScreen = new T();
            //Load the screens content
            pauseScreen.LoadContent();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected void LoadContent()
        {
            //Loads content from the current screen
            currentScreen.LoadContent();

            //load pause screen if not null
            if (pauseScreen != null)
                pauseScreen.LoadContent();

            //Gets the current list of entitys
            // entitys = AssetFactory.Manager.GetList();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected void UnloadContent()
        {
            //Unloads content from the current screen
            currentScreen.UnloadContent();
            EventManager.Manager.RemoveDelegates(currentScreen);

            //unload pause screen if not null
            if (pauseScreen != null)
                pauseScreen.UnloadContent();

        }

        /// <summary>
        /// Changes the screen to the passed in screen
        /// </summary>
        /// <typeparam name="T">Screen of type IGameScreen</typeparam>
        public void ChangeScreen<T>() where T : IGameScreen, new()
        {
            //Unload the current screen
            UnloadContent();
            //Set new screen of the type passed in
            currentScreen = new T();
            //Load the content of the screen
            LoadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update()
        {
            if (currentScreen != null)
            {
                //Update the current screen
                currentScreen.UpdateScreen();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="renderManager">Provides a refrence to the renderManager.</param>
        public void Draw(IRenderManager renderManager)
        {
            if (currentScreen != null)
            {
                if (currentScreen.Camera2D != null)
                    renderManager.StartDraw(
                        SpriteSortMode.Deferred, BlendState.AlphaBlend, null,
                        null, null, null, currentScreen.Camera2D.Transform
                    );
                else
                    renderManager.StartDraw(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                currentScreen.DrawScreen(renderManager);
                renderManager.EndDraw();
            }
        }
    }
}
