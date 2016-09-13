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

        //private filed to hold all resources that have been created
        private IDictionary<String, IDisposable> resources;

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
            resources = new Dictionary<String, IDisposable>();
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
        /// Returns a stored resource or creates a new one if none exsist
        /// </summary>
        /// <param name="path">The path to the resource</param>
        /// <returns>the stored or created resource</returns>
        public T LoadResource<T>(string path)
        {
            T resource;
            //If the resource exsists
            if (resources.ContainsKey(path))
            {
                //Sets the resource to the found resource
                resource = (T)resources[path];
            }
            else
            {
                //Create a new resource
                resource = Content.Load<T>(path);
                //add created resource to store of resources
                resources.Add(path, (IDisposable)resource);
            }
            return resource;
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
