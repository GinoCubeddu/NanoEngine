using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.ObjectTypes.General;

namespace NanoEngine.Factories
{
    public class SceneFactory : ISceneFactory
    {
        /// <summary>
        /// Creates and initalises the requested game scene and returns it
        /// </summary>
        /// <typeparam name="T">The type of scene to create</typeparam>
        /// <returns>The created game scene</returns>
        /// <param name="name">The name of the scene</param>
        public IGameScene CreateScene<T>(string name) where T : IGameScene, new()
        {
            // Create te requested scene type
            IGameScene scene = new T();

            // Init and load the scene
            scene.Initialise(name);
            scene.LoadContent();

            // return the created scene
            return scene;
        }
    }
}
