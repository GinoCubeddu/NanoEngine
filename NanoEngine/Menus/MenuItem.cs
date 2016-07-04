using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Menus
{
    public class MenuItem : IMenuItem
    {
        //Fields containing the two textures
        private Texture2D texture1, texture2;

        //Field containing the position of the menu item
        private Vector2 position;

        private IMenuControler controler;

        /// <summary>
        /// Constructor for the menu item
        /// </summary>
        /// <param name="tex1">Texture for the first state</param>
        /// <param name="tex2">Texture for the second state</param>
        /// <param name="pos">Vector for the position</param>
        public MenuItem(Texture2D tex1, Texture2D tex2, Vector2 pos) 
        {
            texture1 = tex1;
            texture2 = tex2;
            position = pos;
        }

        /// <summary>
        /// Method that initalises the menu with its controler
        /// </summary>
        /// <typeparam name="T">The type of controler to give to the menu</typeparam>
        public void Initialise<T>() where T : IMenuControler, new()
        {
            controler = new T();
            controler.Initalise(this);
        }

        /// <summary>
        /// Getter for the first texture
        /// </summary>
        public Texture2D Texture1
        {
            get { return texture1; }
        }

        /// <summary>
        /// Getter for the second texture
        /// </summary>
        public Texture2D Texture2
        {
            get { return texture2; }
        }

        /// <summary>
        /// Getter for the position of the menu item
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }

        /// <summary>
        /// Getter to return the controler of the menu item
        /// </summary>
        public IMenuControler Controler
        {
            get { return controler; }
        }
    }
}
