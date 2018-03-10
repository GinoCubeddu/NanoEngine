using NanoEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.States
{
    public class HorizontalState<T> : IState<T> where T : IAiComponent
    {
        private readonly string _animationState;

        private readonly int _direction;

        public bool IsSuccess { get; }

        public HorizontalState(string animationState, int direction)
        {
            _animationState = animationState;
            _direction = direction;
        }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter(T owner, IDictionary<string, object> stateArguments)
        {
            Console.WriteLine("Entering WalkingState");
            owner.ControledAsset.AssetAnimation.ChangeAnimationState(_animationState);
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit(T owner)
        {
            Console.WriteLine("Exiting WalkingState");
        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update(T owner)
        {
            (owner.ControledAsset as PhysicsEntity)?.ApplyForce(new Vector2(
                0, 0.2f * _direction
            ));
        }
    }
}
