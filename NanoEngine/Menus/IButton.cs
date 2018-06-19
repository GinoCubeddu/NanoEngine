using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Menus
{
    public interface IButton
    {
        /// <summary>
        /// Getter for the position of the button
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// Getter for the current texture of the button
        /// </summary>
        Texture2D CurrentTexture { get; }

        /// <summary>
        /// Getter for the controler of the button
        /// </summary>
        IButtonControler Controler { get; }

        /// <summary>
        /// Activates the button controler
        /// </summary>
        void Activate();

        /// <summary>
        /// Toggles the texture of the button
        /// </summary>
        void ToggleActiveTexture();

        /// <summary>
        /// Draws the button on screen
        /// </summary>
        /// <param name="renderManager">An instance of the render manager</param>
        void Draw(IRenderManager renderManager);
    }
}
