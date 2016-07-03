using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Core.Managers
{
    class ContentManagerLoad : IContentManagerLoad
    {
        //private feild that holds the instance of the interface
        private static IContentManagerLoad manager;

        //private feild holding the content manager
        private ContentManager Content;

        //private filed to hold all textures that have been created
        private IDictionary<String, Texture2D> resources;

        //public getter for returning the manager
        public static IContentManagerLoad Manager
        {
            get { return manager ?? (manager = new ContentManagerLoad()); }
        }


        /// <summary>
        /// Initilises the content manager
        /// </summary>
        /// <param name="gContent">Content manager</param>
        public void Intinalise(ContentManager gContent)
        {
            Content = gContent;
            Content.RootDirectory = "Content";
            resources = new Dictionary<String, Texture2D>();
        }

        /// <summary>
        /// Unloads everything
        /// </summary>
        public void UnloadAll()
        {
            //calls unload from content manager
            Content.Unload();
            resources.Clear();
        }

        /// <summary>
        /// Loads a resource to use
        /// </summary>
        /// <param name="path">string containing the path to the resource</param>
        /// <returns>Texture2D</returns>
        public T LoadResource<T>(string path)
        {
            return Content.Load<T>(path);
        }

        /// <summary>
        /// Returns a stored texture or creates a new one if none exsist
        /// </summary>
        /// <param name="path">The path to the texture</param>
        /// <returns>the stored or created texture</returns>
        public Texture2D GetTexture(string path)
        {
            //set a null texture
            Texture2D texture = null;
            //If the texture exsists
            if (resources.ContainsKey(path))
            {
                //Sets the texture to the found resource
                texture = resources[path];
            }
            else
            {
                //Create a new texture
                texture = Content.Load<Texture2D>(path);
                //add created texture to store of textures
                resources.Add(path, texture);
            }
            return texture;
        }

        /// <summary>
        /// Method to get the content manager
        /// </summary>
        /// <returns>The current content manager</returns>
        public ContentManager getContentManager()
        {
            return Content;
        }
    }
}
