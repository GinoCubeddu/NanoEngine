using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Menus
{
    public interface IMenuItem
    {
        /// <summary>
        /// Method that initalises the menu with its controler
        /// </summary>
        /// <typeparam name="T">The type of controler to give to the menu</typeparam>
        void Initialise<T>() where T : IMenuControler, new();

        /// <summary>
        /// Getter for the first texture
        /// </summary>
        Texture2D Texture1 { get; }

        /// <summary>
        /// Getter for the second texture
        /// </summary>
        Texture2D Texture2 { get; }

        /// <summary>
        /// Getter for the position of the menu item
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// Getter to return the controler of the menu item
        /// </summary>
        IMenuControler Controler { get; }
    }
}
