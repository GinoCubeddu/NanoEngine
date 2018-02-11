using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing
{
    public class PatrolState : IState
    {
        private readonly IList<string> _patrolAnimations;

        private readonly IList<Vector2> _patrolPoints;

        private int _currentAnimation;

        private int _currentTarget;

        private int _direction;

        private int _pointsVisited;

        public bool IsSuccess { get; private set; }

        public PatrolState(IList<string> patrolAnimations, IList<Vector2> patrolPoints)
        {
            _patrolAnimations = patrolAnimations;
            _patrolPoints = patrolPoints;
            _currentTarget = 0;
            _currentAnimation = 0;
        }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter<T>(T owner)
        {
            IAiComponent _owner = (owner as IAiComponent);
            _pointsVisited = 0;
            IsSuccess = false;
            _owner.ControledAsset.AssetAnimation.ChangeAnimationState(_patrolAnimations[0]);
            if (_patrolPoints[_currentTarget].X - _owner.ControledAsset.Position.X < 0)
                _direction = -1;
            else
                _direction = 1;
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit<T>(T owner)
        {
            _currentTarget = 0;
            _pointsVisited = 0;
            Console.WriteLine("Exiting patrol state");
        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update<T>(T owner)
        {
            IAiComponent _owner = (owner as IAiComponent);
            if (_direction == 1)
            {
                _currentAnimation = 0;
                _owner.ControledAsset.AssetAnimation.ChangeAnimationState(
                    _patrolAnimations[_currentAnimation]
                );
            }
            else
            {
                _currentAnimation = 1;
                _owner.ControledAsset.AssetAnimation.ChangeAnimationState(
                    _patrolAnimations[_currentAnimation]
                );
            }

            float Distance = Vector2.Distance(_owner.ControledAsset.Position, _patrolPoints[_currentTarget]);
            _owner.ControledAsset.SetPosition(new Vector2(
                _owner.ControledAsset.Position.X + 1 * _direction,
                _owner.ControledAsset.Position.Y
            ));

            if (Distance < 10)
            {
                if (_currentTarget + 1 >= _patrolPoints.Count)
                    _currentTarget = 0;
                else
                    _currentTarget++;

                if (_patrolPoints[_currentTarget].X - _owner.ControledAsset.Position.X < 0)
                    _direction = -1;
                else
                    _direction = 1;
                _pointsVisited++;
                if (_pointsVisited == 5)
                    IsSuccess = true;
            }

        }
    }
}
