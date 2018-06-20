using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Locator;

namespace NanoEngine.Core.Interfaces
{
    public interface ISceneManager : IService
    {
        /// <summary>
        /// Adds a screen to the scene manager
        /// </summary>
        /// <typeparam name="T">The type of screen to add</typeparam>
        /// <param name="name">The id of the screen</param>
        void AddScreen<T>(string name) where T : IGameScreen, new();

        /// <summary>
        /// Tells the scene manager to start updating the screen
        /// </summary>
        /// <param name="name">The id of the screen to start updating</param>
        void StartUpdatingScreen(string name);

        /// <summary>
        /// Tells the scenemnager to stop updating a screen
        /// </summary>
        /// <param name="name">The id of the screen to stop updating</param>
        void StopUpdatingScreen(string name);

        /// <summary>
        /// Tells the scene manager to reload a screen
        /// </summary>
        /// <param name="name">The id of the screen to reload</param>
        void ReloadScreen(string name);

        /// <summary>
        /// Tells the scenemanager to delete a scene
        /// </summary>
        /// <param name="name">The id of the scene to delete</param>
        void DeleteScreen(string name);

        /// <summary>
        /// Method to draw the objects on the screen
        /// </summary>
        /// <param name="renderManager">Provides a refrence to the renderManager.</param>
        void Draw(IRenderManager renderManager);

        /// <summary>
        /// Method to update the objects on the screen
        /// </summary>
        /// <param name="updateManager">Provides a refrence to the updateManager.</param>
        void Update(IUpdateManager updateManager);
    }
}
