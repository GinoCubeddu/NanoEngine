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
        /// Loads the texture 2D 
        /// </summary>
        /// <param name="path">string containing the path to the texture</param>
        /// <returns>Texture2D</returns>
        T LoadResource<T>(string path);

        /// <summary>
        /// Unloads everything
        /// </summary>
        void UnloadAll();

        /// <summary>
        /// Returns a stored texture or creates a new one if none exsist
        /// </summary>
        /// <param name="path">The path to the texture</param>
        /// <returns>the stored or created texture</returns>
        Texture2D GetTexture(string path);
    }
}
