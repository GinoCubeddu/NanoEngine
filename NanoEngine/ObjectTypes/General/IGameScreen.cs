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
        /// Method to update the current screen
        /// </summary>
        void Update();

        /// <summary>
        /// Method to draw the current screen
        /// </summary>
        void Draw();
    }
}
