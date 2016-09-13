using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Core.Interfaces
{
    public interface IContentManagerLoad
    {
        /// <summary>
        /// Intilises the contentmanager
        /// </summary>
        /// <param name="gContent"></param>
        void Intinalise(ContentManager gContent);

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
