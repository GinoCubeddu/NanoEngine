using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    public interface IAnimation
    {
        /// <summary>
        /// Draws the animation for the asset
        /// </summary>
        /// <param name="renderManager">RenderManager instance required to draw</param>
        void Animate(IRenderManager renderManager);

        /// <summary>
        /// Adds a state to the animation dict giving the animation a default
        /// frame rate of 12 frames per second.
        /// </summary>
        /// <param name="stateName">A unique name for the state</param>
        /// <param name="frameWidth">The width for the animation frame</param>
        /// <param name="frameHeight">The height for the animation frame</param>
        /// <param name="spriteSheetRow">The row within the spritesheet</param>
        /// <param name="animationFrameCount">The amount of frames the animation has</param>
        void AddState(
            string stateName, int frameWidth, int frameHeight,
            int spriteSheetRow, int animationFrameCount
        );

        /// <summary>
        /// Adds a state to the animation dict
        /// </summary>
        /// <param name="stateName">A unique name for the state</param>
        /// <param name="frameWidth">The width for the animation frame</param>
        /// <param name="frameHeight">The height for the animation frame</param>
        /// <param name="spriteSheetRow">The row within the spritesheet</param>
        /// <param name="animationFrameCount">The amount of frames the animation has</param>
        /// <param name="framerate">The framerate of the animation</param>
        void AddState(
            string stateName, int frameWidth, int frameHeight,
            int spriteSheetRow, int animationFrameCount, int framerate
        );

        /// <summary>
        /// Tells the animation to change to a different state of animation
        /// </summary>
        /// <param name="animationState">The state we want to change to</param>
        void ChangeAnimationState(string animationState);
    }
}
