using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing
{
    public class IdleState : IState
    {
        private string _animationState;

        public bool IsSuccess { get; }

        public IdleState(string animationState)
        {
            _animationState = animationState;
        }
        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter<T>(T owner)
        {
            Console.WriteLine("Exiting IdleState");
            (owner as IAiComponent).ControledAsset.AssetAnimation.ChangeAnimationState(_animationState);
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit<T>(T owner)
        {
            Console.WriteLine("Exiting IdleState");
        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update<T>(T owner)
        {
            Console.WriteLine("Updating idle state");
        }
    }
}
