using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Locator;

namespace NanoEngine.Core.Interfaces
{
    public interface INanoContentManager : IService
    {
        /// <summary>
        /// Unloads everything
        /// </summary>
        void UnloadAll();

        /// <summary>
        /// Returns a stored resource or creates a new one if none exsist
        /// </summary>
        /// <param name="path">The path to the resource</param>
        /// <returns>the stored or created resource</returns>
        T LoadResource<T>(string path);
    }
}
