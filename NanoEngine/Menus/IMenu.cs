using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.Menus
{
    public interface IMenu
    {
        string Name { get; }

        /// <summary>
        /// Adds a button to the menu
        /// </summary>
        /// <param name="button"></param>
        void AddButton(IButton button);

        /// <summary>
        /// Draws the buttons within the menu
        /// </summary>
        /// <param name="renderManager">An instance of the render manager</param>
        void Draw(IRenderManager renderManager);

        /// <summary>
        /// Sets the key to activate the button
        /// </summary>
        /// <param name="key">The activator</param>
        void SetActivateButton(Keys key);

        /// <summary>
        /// Sets the sound effect for the menu
        /// </summary>
        /// <param name="soundEffect">The name of the sound effect</param>
        void SetSoundEffect(string soundEffect);
    }
}
