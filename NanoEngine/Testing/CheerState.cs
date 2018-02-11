using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing
{
    class CheerState : IState
    {
        public bool IsSuccess { get; private set; }

        private string _animation;

        public int Timer;

        public CheerState(string animationState)
        {
            _animation = animationState;
        }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter<T>(T owner)
        {
            IsSuccess = false;
            IAiComponent _owner = (owner as IAiComponent);
            _owner.ControledAsset.AssetAnimation.ChangeAnimationState(_animation);
            Timer = 0;
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit<T>(T owner)
        {

        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update<T>(T owner)
        {
            Timer++;
            if (Timer > 360)
                IsSuccess = true;
        }
    }
}
