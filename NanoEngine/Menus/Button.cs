using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.Menus
{
    public class Button<T> : IButton where T : IButtonControler, new()
    {
        /// <summary>
        /// Getter for the current texture of the button
        /// </summary>
        public Texture2D CurrentTexture { get; protected set; }

        /// <summary>
        /// Getter for the controler of the button
        /// </summary>
        public IButtonControler Controler { get; }

        // List to hold the texture of the 
        protected IList<Texture2D> Textures;

        // The current position in the list for the texture
        protected int CurrentTexturePosition;

        // The position for the button
        public Vector2 Position { get; protected set; }

        public Button(Texture2D textureOne, Texture2D textureTwo, Vector2 position)
        {
            // Create an instance of the controler
            Controler = new T();

            // Create the textures list and set the texture to 0
            Textures = new List<Texture2D> {textureOne};
            CurrentTexturePosition = 0;
            CurrentTexture = textureOne;

            // If a second texture was passed then add it
            if (textureTwo != null)
                Textures.Add(textureTwo);

            // Set the position
            Position = position;
        }

        public Button(Texture2D textureOne, Vector2 position) : this(textureOne, null, position)
        {
        }

        /// <summary>
        /// Activates the button controler
        /// </summary>
        public virtual void Activate()
        {
            Controler.Activate();
        }

        /// <summary>
        /// Toggles the texture of the button
        /// </summary>
        public virtual void ToggleActiveTexture()
        {
            // If there is only 1 texture then we cant toggle them
            if (Textures.Count == 1)
                return;

            // Decide which texture to switch to
            CurrentTexturePosition = CurrentTexturePosition == 0 ? 1 : 0;
            CurrentTexture = Textures[CurrentTexturePosition];
        }

        /// <summary>
        /// Draws the button on screen
        /// </summary>
        /// <param name="renderManager">An instance of the render manager</param>
        public virtual void Draw(IRenderManager renderManager)
        {
            renderManager.Draw(
                CurrentTexture,
                Position,
                null,
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1
            );
        }
    }
}
