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
        private IDictionary<string, IDictionary<string, int>> _states;

        // The current animation state
        private string _currentAninmation;

        // The current Column within the animation
        private int _currentColumn;

        // The asset that is being animated
        private readonly IAsset _animatedAsset;

        private float _timer;

        private float fps;

        public Animation(IAsset asset)
        {
            _states = new Dictionary<string, IDictionary<string, int>>();
            _currentColumn = 0;
            _animatedAsset = asset;
            _timer = 0;
            fps = 1f / 12f;
        }

        /// <summary>
        /// Draws the animation for the asset
        /// </summary>
        /// <param name="renderManager">RenderManager instance required to draw</param>
        public void Animate(IRenderManager renderManager)
        {
            IDictionary<string, int> animationData = _states[_currentAninmation];
            _timer += (float) renderManager.gameTime.ElapsedGameTime.TotalSeconds;
            _animatedAsset.Bounds = new Rectangle((int)_animatedAsset.Position.X, (int)_animatedAsset.Position.Y, animationData["TextureWidth"], animationData["TextureHeight"]);
            if (_timer > fps)
            {
                _currentColumn++;

                if (_currentColumn >= animationData["ColumnCount"])
                    _currentColumn = 0;
                _timer = 0;
            }
            
            renderManager.Draw(
                    _animatedAsset.Texture, _animatedAsset.Position,
                    new Rectangle(
                        _currentColumn * animationData["TextureWidth"],
                        animationData["TextureHeight"] * animationData["Row"],
                        animationData["TextureWidth"],
                        animationData["TextureHeight"]
                    ),
                    Color.White
                );
        }

        /// <summary>
        /// Adds a state to the animation dict giving the animation a default
        /// frame rate of 12 frames per second.
        /// </summary>
        /// <param name="stateName">A unique name for the state</param>
        /// <param name="frameWidth">The width for the animation frame</param>
        /// <param name="frameHeight">The height for the animation frame</param>
        /// <param name="spriteSheetRow">The row within the spritesheet</param>
        /// <param name="animationFrameCount">The amount of frames the animation has</param>
        public void AddState(
            string stateName, int frameWidth, int frameHeight,
            int spriteSheetRow, int animationFrameCount
        )
        {
            AddState(
                stateName, frameWidth, frameHeight, spriteSheetRow,
                animationFrameCount, 12
            );
        }

        /// <summary>
        /// Adds a state to the animation dict
        /// </summary>
        /// <param name="stateName">A unique name for the state</param>
        /// <param name="frameWidth">The width for the animation frame</param>
        /// <param name="frameHeight">The height for the animation frame</param>
        /// <param name="spriteSheetRow">The row within the spritesheet</param>
        /// <param name="animationFrameCount">The amount of frames the animation has</param>
        /// <param name="framerate">The framerate of the animation</param>
        public void AddState(
            string stateName, int frameWidth, int frameHeight,
            int spriteSheetRow, int animationFrameCount, int framerate
        )
        {
            if (_states.ContainsKey(stateName))
                throw new ArgumentException(string.Format(
                    "The animation state of {0} already exsists for the " + 
                    "{1} asset, please use unique names per asset for " + 
                    "the animation states", stateName, _animatedAsset.UniqueName
                ));

            if (_states.Count == 0)
                _currentAninmation = stateName;

            _states.Add(stateName, new Dictionary<string, int>());
            _states[stateName].Add("TextureWidth", frameWidth);
            _states[stateName].Add("TextureHeight", frameHeight);
            _states[stateName].Add("Row", spriteSheetRow);
            _states[stateName].Add("ColumnCount", animationFrameCount);
            _states[stateName].Add("framerate", framerate);
        }

        /// <summary>
        /// Tells the animation to change to a different state of animation
        /// </summary>
        /// <param name="animationState">The state we want to change to</param>
        public void ChangeAnimationState(string animationState)
        {
            if (animationState == _currentAninmation)
                return;

            if (!_states.Keys.Contains(animationState))
                throw new Exception(
                    string.Format(
                        "The animation state with the id {0} does not " +
                        "exsist. The avaliable options are: {1}",
                        animationState, _states.Keys.ToString()
                    )
                );
            _currentAninmation = animationState;
            _currentColumn = 0;
            fps = 1f / (float) _states[_currentAninmation]["framerate"];
        }
    }
}
