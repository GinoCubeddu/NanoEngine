using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.ObjectTypes.General;

namespace NanoEngine.Factories
{
    public interface ISceneFactory
    {
        /// <summary>
        /// Creates and initalises the requested game scene and returns it
        /// </summary>
        /// <typeparam name="T">The type of scene to create</typeparam>
        /// <param name="name">The name of the scene</param>
        /// <returns>The created game scene</returns>
        IGameScene CreateScene<T>(String name) where T : IGameScene, new();
    }
}
