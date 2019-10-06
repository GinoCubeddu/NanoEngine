﻿using System;
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
        /// Creates and initalises the requested game screen and returns it
        /// </summary>
        /// <typeparam name="T">The type of screen to create</typeparam>
        /// <returns>The created game screen</returns>
        /// <param name="name">The name of the screen</param>
        public IGameScreen CreateScreen<T>(string name) where T : IGameScreen, new()
        {
            // Create te requested screen type
            IGameScreen screen = new T();

            // Init and load the screen
            screen.Initialise(name);
            screen.LoadContent();

            // return the created screen
            return screen;
        }
    }
}
