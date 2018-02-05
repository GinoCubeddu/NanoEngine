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
        /// Tells the animation to change to a different state of animation
        /// </summary>
        /// <param name="animationState">The state we want to change to</param>
        void ChangeAnimationState(string animationState);
    }
}
