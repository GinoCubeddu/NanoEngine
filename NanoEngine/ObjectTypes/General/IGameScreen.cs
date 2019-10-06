using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Camera;

namespace NanoEngine.ObjectTypes.General
{
    public interface IGameScreen
    {
        // Returns the current camera of the screen
        ICamera2D Camera2D { get; }

        // Returns the name of the screen
        String ScreenName { get; }

        // Returns the current amount of update time this screen has had
        double LevelTimer { get; set; }

        /// <summary>
        /// Method to load the staring content of the screen
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Method to unload the content of the screen
        /// </summary>
        void UnloadContent();

        /// <summary>
        /// Method to Draw the scene
        /// </summary>
        /// <param name="renderManager">RenderManager containing draw methods</param>
        void DrawScreen(IRenderManager renderManager);

        /// <summary>
        /// Method to update the GameScreen
        /// </summary>
        /// <param name="updateManager">Provides a refrence to the updateManager.</param>
        void UpdateScreen(IUpdateManager updateManager);

        /// <summary>
        /// Initialise's the screen
        /// </summary>
        /// <param name="name">The name of the screen</param>
        void Initialise(String name);
    }
}
