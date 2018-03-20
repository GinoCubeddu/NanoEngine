using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace NanoEngine.Core.Managers
{
    public class NanoContentManager : INanoContentManager
    {
        //private feild holding the content manager
        private ContentManager _content;

        //private filed to hold all textures that have been created
        private IDictionary<string, IDisposable> _resources;

        /// <summary>
        /// Constructor of the NanoContentManager
        /// </summary>
        /// <param name="gContent">An instance of monogames content manager</param>
        public NanoContentManager(ContentManager gContent)
        {
            _content = gContent;
            _content.RootDirectory = "Content";
            _resources = new Dictionary<string, IDisposable>();
        }

        /// <summary>
        /// Unloads everything
        /// </summary>
        public void UnloadAll()
        {
            //calls unload from content manager
            _content.Unload();
            _resources.Clear();
        }

        /// <summary>
        /// Loads a resource to use
        /// </summary>
        /// <param name="path">string containing the path to the resource</param>
        /// <returns>Texture2D</returns>
        public T LoadResource<T>(string path)
        {
            // If the resource is of type IDisposable then we can load it into
            // our saved resourses
            if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
            {
                // If the resource exsists then return it
                if (_resources.ContainsKey(path))
                    return (T)_resources[path];
               
                // Load the resourse into the dict
                _resources[path] = (IDisposable)_content.Load<T>(path);

                // Return the loaded dict
                return (T)_resources[path];
            }

            // Return the requested resource
            return _content.Load<T>(path);
        }
    }
}
