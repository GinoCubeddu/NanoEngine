﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing
{
    class ChaseState<T> : IState<T> where T : IAiComponent
    {
        public bool IsSuccess { get; }

        // Animation state to be in when facing right
        private readonly string _rightFacingAnimation;

        // Animation state to be in when facing left
        private readonly string _leftFacingAnimation;

        private IAsset _chasedAsset;

        private int _direction;

        public ChaseState(string rightAnimation, string leftAnimation, IAsset chasedAsset)
        {
            _rightFacingAnimation = rightAnimation;
            _leftFacingAnimation = leftAnimation;
            _chasedAsset = chasedAsset;
        }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter(T owner)
        {
            Console.WriteLine("Entering chase state");
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit(T owner)
        {
            Console.WriteLine("Exiting chase state");
        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update(T owner)
        {
            if (_chasedAsset.Position.X - owner.ControledAsset.Position.X < 0)
            {
                owner.ControledAsset.AssetAnimation.ChangeAnimationState(_leftFacingAnimation);
                _direction = -1;
            }
            else
            {
                _direction = 1;
                owner.ControledAsset.AssetAnimation.ChangeAnimationState(_rightFacingAnimation);
            }


            owner.ControledAsset.SetPosition(new Vector2(
                owner.ControledAsset.Position.X + 1 * _direction,
                owner.ControledAsset.Position.Y
            ));
        }
    }
}
