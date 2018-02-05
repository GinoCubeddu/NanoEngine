using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine
{
    public class Animation : IAnimation
    {
        // Dictonary to hold the states
        private IDictionary<string, IDictionary<string, int>> States;

        // The current animation state
        private string CurrentAninmation;

        // The current Column within the animation
        private int CurrentColumn;

        // The asset that is being animated
        private IAsset _animatedAsset;

        private int Timer;

        public Animation(
            IAsset asset, IDictionary<string, IDictionary<string, int>> states
        )
        {
            States = states;
            CurrentAninmation = states.Keys.First();
            CurrentColumn = 0;
            _animatedAsset = asset;
            Timer = 0;
        }

        /// <summary>
        /// Draws the animation for the asset
        /// </summary>
        /// <param name="renderManager">RenderManager instance required to draw</param>
        public void Animate(IRenderManager renderManager)
        {
            IDictionary<string, int> AnimationData = States[CurrentAninmation];
            renderManager.Draw(
                    _animatedAsset.Texture, _animatedAsset.Position,
                    new Rectangle(
                        CurrentColumn * AnimationData["TextureWidth"],
                        AnimationData["TextureHeight"] * AnimationData["Row"],
                        AnimationData["TextureWidth"],
                        AnimationData["TextureHeight"]
                    ),
                    Color.White
                );
            if (Timer > 3)
            {
                CurrentColumn++;

                if (CurrentColumn >= AnimationData["ColumnCount"])
                    CurrentColumn = 0;
                Timer = 0;
            }
            Timer++;

        }

        /// <summary>
        /// Tells the animation to change to a different state of animation
        /// </summary>
        /// <param name="animationState">The state we want to change to</param>
        public void ChangeAnimationState(string animationState)
        {
            if (animationState == CurrentAninmation)
                return;

            if (!States.Keys.Contains(animationState))
                throw new Exception(
                    string.Format(
                        "The animation state with the id {0} does not " +
                        "exsist. The avaliable options are: {1}",
                        animationState, States.Keys.ToString()
                    )
                );
            CurrentAninmation = animationState;
            CurrentColumn = 0;
        }
    }
}
