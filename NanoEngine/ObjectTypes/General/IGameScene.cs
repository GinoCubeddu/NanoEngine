using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Camera;

namespace NanoEngine.ObjectTypes.General
{
    public interface IGameScene
    {
        // Returns the current camera of the scene
        ICamera2D Camera2D { get; }

        // Returns the name of the scene
        String SceneName { get; }

        // Returns the current amount of update time this scene has had
        double LevelTimer { get; set; }

        /// <summary>
        /// Method to load the staring content of the scene
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Method to unload the content of the scene
        /// </summary>
        void UnloadContent();

        /// <summary>
        /// Method to Draw the scene
        /// </summary>
        /// <param name="renderManager">RenderManager containing draw methods</param>
        void DrawScene(IRenderManager renderManager);

        /// <summary>
        /// Method to update the GameScene
        /// </summary>
        /// <param name="updateManager">Provides a refrence to the updateManager.</param>
        void UpdateScene(IUpdateManager updateManager);

        /// <summary>
        /// Initialise's the scene
        /// </summary>
        /// <param name="name">The name of the scene</param>
        void Initialise(String name);
    }
}
