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
        // Animation state to be in when facing right
        private readonly string _rightFacingAnimation;

        // Animation state to be in when facing left
        private readonly string _leftFacingAnimation;

        // The points that the AI will patrol around
        private readonly IList<Vector2> _patrolPoints;

        // Contains the index for the vector that the Ai is moving towards
        private int _currentTarget;

        // The direction of the point
        private int _direction;

        // The amount of points that have been patroled
        private int _pointsVisited;

        public bool IsSuccess { get; private set; }

        public PatrolState(string rightAnimation, string leftAnimation, IList<Vector2> patrolPoints)
        {
            _rightFacingAnimation = rightAnimation;
            _leftFacingAnimation = leftAnimation;
            _patrolPoints = patrolPoints;
            _currentTarget = 0;
        }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter<T>(T owner)
        {
            // Cast the owner as Ai
            IAiComponent _owner = (owner as IAiComponent);
            // Reset all vars 
            _pointsVisited = 0;
            IsSuccess = false;
            _owner.ControledAsset.AssetAnimation.ChangeAnimationState(_rightFacingAnimation);
            CheckDirection(_owner);
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
            // Cast the owner as an AI
            IAiComponent _owner = (owner as IAiComponent);

            // Move the asset towards the point
            _owner.ControledAsset.SetPosition(new Vector2(
                _owner.ControledAsset.Position.X + 1 * _direction,
                _owner.ControledAsset.Position.Y
            ));

            // If the distance is less than 10 then it is at the point
            if (Vector2.Distance(_owner.ControledAsset.Position, _patrolPoints[_currentTarget]) < 10)
            {
                // Change target
                if (_currentTarget + 1 >= _patrolPoints.Count)
                    _currentTarget = 0;
                else
                    _currentTarget++;
                // Check the direction of the next point
                CheckDirection(_owner);
                // Make the state a success if the asset has patroled 5 points
                _pointsVisited++;
                if (_pointsVisited == 5)
                    IsSuccess = true;
            }

        }

        private void CheckDirection(IAiComponent owner)
        {
            if (_patrolPoints[_currentTarget].X - owner.ControledAsset.Position.X < 0)
            {
                owner.ControledAsset.AssetAnimation.ChangeAnimationState(
                    _leftFacingAnimation
                );
                _direction = -1;
            }
            else
            {
                owner.ControledAsset.AssetAnimation.ChangeAnimationState(
                    _rightFacingAnimation
                );
                _direction = 1;
            }
        }
    }
}
