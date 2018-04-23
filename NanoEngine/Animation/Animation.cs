using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Animation
{
    public class Animation : IAnimation
    {
        // Dictonary to hold the states
        protected IDictionary<string, AnimationState> _states;

        // The current animation state
        protected string _currentAninmation;

        // The asset that is being animated
        protected readonly IAsset _animatedAsset;

        protected float _timer;

        public Animation(IAsset asset)
        {
            _states = new Dictionary<string, AnimationState>();
            _animatedAsset = asset;
            _timer = 0;
        }

        /// <summary>
        /// Draws the animation for the asset
        /// </summary>
        /// <param name="renderManager">RenderManager instance required to draw</param>
        public virtual void Animate(IRenderManager renderManager)
        {
            AnimationState animationData = _states[_currentAninmation];

            // Update the timer
            _timer += (float) renderManager.gameTime.ElapsedGameTime.TotalSeconds;

            // Set the bounds of the controled asset
            _animatedAsset.Bounds = new Rectangle((int) _animatedAsset.Position.X, (int) _animatedAsset.Position.Y,
                animationData.GetCurrentFrame().Width, animationData.GetCurrentFrame().Height);

            // If it is time to move to the next frame
            if (_timer > _states[_currentAninmation].FrameRate)
            {
                // Set the frame incriment to 1
                int frameIncriment = 1;

                // If we are running behind then work out how far we are behind
                if (_timer > _states[_currentAninmation].FrameRate * 2)
                {
                    // Set the frame incriment to the rounded value of the current timer
                    // divided by the correct frame rate
                    frameIncriment = (int)Math.Round(_timer / _states[_currentAninmation].FrameRate);
                }

                // Tell the AnimationState to update by the calcualated frames
                animationData.ChangePosition(frameIncriment);

                 // Reset the timer to 0
                _timer = 0;
            }
            
            renderManager.Draw(
                _animatedAsset.Texture, _animatedAsset.Position, animationData.GetCurrentFrame(),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, _animatedAsset.DrawLevel
            );

        }

        /// <summary>
        /// Adds a state to the animation dict
        /// </summary>
        /// <param name="stateName">A unique name for the state</param>
        /// <param name="frameWidth">The width for the animation frame</param>
        /// <param name="frameHeight">The height for the animation frame</param>
        /// <param name="animationFrameCount">The amount of frames the animation has</param>
        /// <param name="framerate">The framerate of the animation</param>
        public virtual void AddState(
            string stateName, int frameWidth, int frameHeight,
           int animationFrameCount, int framerate = 12, int yPos = 0, int startX = 0
        )
        {
            // Call the Craete state method to register it
            CreateState(stateName, framerate);

            // For loop through the animationFramCount and add a new frame to the animation
            // using the information provided
            for (int i = 0; i < animationFrameCount; i++)
            {
                _states[stateName].AddFrame(new Rectangle(startX + i * frameWidth, yPos * frameHeight, frameWidth, frameHeight));
            }
        }

        /// <summary>
        /// Creates a state and adds it to the dictonary
        /// </summary>
        /// <param name="stateName">The name of the state</param>
        /// <param name="framerate">The framerate of the annimation</param>
        private void CreateState(string stateName, int framerate)
        {
            // Throw an error if the key exsists
            if (_states.ContainsKey(stateName))
                throw new ArgumentException(string.Format(
                    "The animation state of {0} already exsists for the " +
                    "{1} asset, please use unique names per asset for " +
                    "the animation states", stateName, _animatedAsset.UniqueName
                ));

            // If there are no states this must be the prime state
            if (_states.Count == 0)
                _currentAninmation = stateName;

            // Create a new Animation state
            _states.Add(stateName, new AnimationState(framerate));
        }

        /// <summary>
        /// Adds an empty state to the animation
        /// </summary>
        /// <param name="stateName">The name of the state</param>
        /// <param name="frameRate">The correct frame rate</param>
        public virtual void CreateEmptyState(string stateName, int frameRate)
        {
            // Creates an empty state within the animation states
            CreateState(stateName, frameRate);
        }

        /// <summary>
        /// Adds a frame to the requested animation state
        /// </summary>
        /// <param name="stateName">The state to be added to</param>
        /// <param name="frame">The frame rectangle</param>
        public virtual void AddFrameToState(string stateName, Rectangle frame)
        {
            // If the requested state does not exsist throw an error
            if (!_states.ContainsKey(stateName))
                throw new KeyNotFoundException("No state by the id " + stateName + " found.");
            // If the state does exsist add the frame
            _states[stateName].AddFrame(frame);
        }

        /// <summary>
        /// Tells the animation to change to a different state of animation
        /// </summary>
        /// <param name="animationState">The state we want to change to</param>
        public virtual void ChangeAnimationState(string animationState)
        {
            // If attempting to change to itsself then return
            if (animationState == _currentAninmation)
                return;

            // throw error if state does not exsist
            if (!_states.Keys.Contains(animationState))
                throw new Exception(
                    string.Format(
                        "The animation state with the id {0} does not " +
                        "exsist. The avaliable options are: {1}",
                        animationState, _states.Keys.ToString()
                    )
                );
            // Set the new animation state
            _currentAninmation = animationState;
        }
    }
}
