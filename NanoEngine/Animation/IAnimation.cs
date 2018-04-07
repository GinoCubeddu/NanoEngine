using Microsoft.Xna.Framework;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.Animation
{
    public interface IAnimation
    {
        /// <summary>
        /// Draws the animation for the asset
        /// </summary>
        /// <param name="renderManager">RenderManager instance required to draw</param>
        void Animate(IRenderManager renderManager);

        /// <summary>
        /// Adds a state to the animation dict
        /// </summary>
        /// <param name="stateName">A unique name for the state</param>
        /// <param name="frameWidth">The width for the animation frame</param>
        /// <param name="frameHeight">The height for the animation frame</param>
        /// <param name="animationFrameCount">The amount of frames the animation has</param>
        /// <param name="framerate">The framerate of the animation</param>
        /// <param name="yPos">The y position that the frames will share</param>
        /// <param name="startX">The start position on the x axis of the texture</param>
        void AddState(
            string stateName, int frameWidth, int frameHeight,
            int animationFrameCount, int framerate, int yPos, int startX
        );

        /// <summary>
        /// Tells the animation to change to a different state of animation
        /// </summary>
        /// <param name="animationState">The state we want to change to</param>
        void ChangeAnimationState(string animationState);



        /// <summary>
        /// Adds an empty state to the animation
        /// </summary>
        /// <param name="stateName">The name of the state</param>
        /// <param name="frameRate">The correct frame rate</param>
        void CreateEmptyState(string stateName, int frameRate);

        /// <summary>
        /// Adds a frame to the requested animation state
        /// </summary>
        /// <param name="stateName">The state to be added to</param>
        /// <param name="frame">The frame rectangle</param>
        void AddFrameToState(string stateName, Rectangle frame);
    }
}
