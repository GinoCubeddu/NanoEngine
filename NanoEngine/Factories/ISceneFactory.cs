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
        /// Creates and initalises the requested game screen and returns it
        /// </summary>
        /// <typeparam name="T">The type of screen to create</typeparam>
        /// <param name="name">The name of the screen</param>
        /// <returns>The created game screen</returns>
        IGameScreen CreateScreen<T>(String name) where T : IGameScreen, new();
    }
}
