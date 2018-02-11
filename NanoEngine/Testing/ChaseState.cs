﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing
{
    class ChaseState : IState
    {
        public bool IsSuccess { get; }

        private IAsset _chasedAsset;

        private int _direction;

        public ChaseState(IAsset chasedAsset)
        {
            _chasedAsset = chasedAsset;
        }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter<T>(T owner)
        {
            Console.WriteLine("Entering chase state");
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit<T>(T owner)
        {
            Console.WriteLine("Exiting chase state");
        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update<T>(T owner)
        {
            IAiComponent _owner = (owner as IAiComponent);
            if (_chasedAsset.Position.X - _owner.ControledAsset.Position.X < 0)
                _direction = -1;
            else
                _direction = 1;

            _owner.ControledAsset.SetPosition(new Vector2(
                _owner.ControledAsset.Position.X + 1 * _direction,
                _owner.ControledAsset.Position.Y
            ));
        }
    }
}
