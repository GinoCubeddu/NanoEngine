using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Core.Interfaces
{
    public interface ISceneManager
    {
        /// <summary>
        /// Changes the screen to the passed in screen
        /// </summary>
        /// <typeparam name="T">Screen of type IGameScreen</typeparam>
        void ChangeScreen<T>() where T : IGameScreen, new();

        //Getter to return the dimentions of the screen
        Vector2 ScreenDimentions { get; }

        /// <summary>
        /// Method that sets the games starting screen
        /// </summary>
        /// <typeparam name="T">The type of screen to be set</typeparam>
        void setStartScreen<T>() where T : IGameScreen, new();

        /// <summary>
        /// Method that sets the pause screen of the game
        /// </summary>
        /// <typeparam name="T">The object type of the screen</typeparam>
        void setPauseScreen<T>() where T : IGameScreen, new();

        /// <summary>
        /// Method to draw the objects on the screen
        /// </summary>
        /// <param name="renderManager">Provides a refrence to the renderManager.</param>
        void Draw(IRenderManager renderManager);

        /// <summary>
        /// Method to update the objects on the screen
        /// </summary>
        void Update();
    }
}
