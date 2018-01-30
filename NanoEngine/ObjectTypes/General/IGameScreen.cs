using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.General
{
    public interface IGameScreen
    {
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
        void UpdateScreen();
    }
}
