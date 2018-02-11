using NanoEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing
{
    public class WalkingState : IState
    {
        private readonly string _animationState;

        private readonly int _direction;

        public WalkingState(string animationState, int direction)
        {
            _animationState = animationState;
            _direction = direction;
        }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter<T>(T owner)
        {
            Console.WriteLine("Entering WalkingState");
            (owner as IAiComponent).ControledAsset.AssetAnimation.ChangeAnimationState(_animationState);
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit<T>(T owner)
        {
            Console.WriteLine("Exiting WalkingState");
        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update<T>(T owner)
        {
            IAiComponent _owner = (owner as IAiComponent);
            _owner.ControledAsset.SetPosition(new Vector2(
                _owner.ControledAsset.Position.X + 1 * _direction,
                _owner.ControledAsset.Position.Y
            ));
        }
    }
}
