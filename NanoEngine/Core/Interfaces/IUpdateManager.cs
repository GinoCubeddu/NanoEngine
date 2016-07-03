using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Core.Interfaces
{
    public interface IUpdateManager
    {
        /// <summary>
        /// Public getter for the current game state
        /// </summary>
        CurrentGameState GameState { get; set; }

        /// <summary>
        /// Public setter for the pause button
        /// </summary>
        Keys PauseButton { set; }

        /// <summary>
        /// Method allows us to change the amount of times the program loops in one second
        /// </summary>
        /// <param name="callsPerSecond">How many times per second</param>
        void ChangeGameLoopAmount(int callsPerSecond);

        /// <summary>
        /// Method to exit the game
        /// </summary>
        void ExitGame();
    }
}
