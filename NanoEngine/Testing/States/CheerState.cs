﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.States
{
    class CheerState<T> : IState<T> where T : IAiComponent
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
        public void Enter(T owner)
        {
            IsSuccess = false;
            owner.ControledAsset.AssetAnimation.ChangeAnimationState(_animation);
            Timer = 0;
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit(T owner)
        {

        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update(T owner)
        {
            Timer++;
            if (Timer > 360)
                IsSuccess = true;
        }
    }
}
