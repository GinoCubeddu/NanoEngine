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
        /// Adds a scene to the scene manager
        /// </summary>
        /// <typeparam name="T">The type of scene to add</typeparam>
        /// <param name="name">The id of the scene</param>
        void AddScene<T>(string name) where T : IGameScene, new();

        /// <summary>
        /// Tells the scene manager to start updating the scene
        /// </summary>
        /// <param name="name">The id of the scene to start updating</param>
        void StartUpdatingScene(string name);

        /// <summary>
        /// Tells the scenemnager to stop updating a scene
        /// </summary>
        /// <param name="name">The id of the scene to stop updating</param>
        void StopUpdatingScene(string name);

        /// <summary>
        /// Tells the scene manager to reload a scene
        /// </summary>
        /// <param name="name">The id of the scene to reload</param>
        void ReloadScene(string name);

        /// <summary>
        /// Tells the scenemanager to delete a scene
        /// </summary>
        /// <param name="name">The id of the scene to delete</param>
        void DeleteScene(string name);

        /// <summary>
        /// Method to draw the objects on the scene
        /// </summary>
        /// <param name="renderManager">Provides a refrence to the renderManager.</param>
        void Draw(IRenderManager renderManager);

        /// <summary>
        /// Method to update the objects on the scene
        /// </summary>
        /// <param name="updateManager">Provides a refrence to the updateManager.</param>
        void Update(IUpdateManager updateManager);
    }
}
