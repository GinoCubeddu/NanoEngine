using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.StateManagement.States;

namespace NanoEngine.StateManagement.Transitions
{
    internal class TransitionHolder : ITransitionHolder
    {
        // Holds all the keyboard transitions for this state
        private IList<KeyboardStateTransition> _keyboardTransitions;
        
        // Holds all the method transitions for this state
        private IList<MethodStateTransition> _methodTransitions;

        private IList<CollisionTransition> _collisionTransitions;

        // Holds the state for the state to transition to if it is a success
        public string SuccessState { get; set; }

        public TransitionHolder()
        {
            _keyboardTransitions = null;
            _methodTransitions = null;
            _collisionTransitions = null;
            SuccessState = null;
        }

        /// <summary>
        /// Adds a keyboard transition to the holder
        /// </summary>
        /// <param name="stateTo">Where the state should transition to</param>
        /// <param name="keyboardType">The type of keyboard event</param>
        /// <param name="keys">The list of keys to be checked</param>
        public void AddKeyboardTransition(string stateTo, KeyStates keyboardType, IList<Keys> keys)
        {
            // Init the keyboard transition list if it is not already created
            if (_keyboardTransitions == null)
                _keyboardTransitions = new List<KeyboardStateTransition>();

            // Add the new state
            _keyboardTransitions.Add(new KeyboardStateTransition(
                stateTo, keyboardType, keys
            ));
        }

        /// <summary>
        /// Checks to see if any of the keyboard transitions are valid
        /// </summary>
        /// <param name="keyboardEvent">The event args given by the handler</param>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        public string CheckKeyboardTransitions(NanoKeyboardEventArgs keyboardEvent)
        {
            // If there are any keyboard transistions
            if (_keyboardTransitions == null)
                return null;

            // loop through each keyboard transition and check for a valid one
            foreach (var transition in _keyboardTransitions)
                if (transition.CheckTransition(keyboardEvent))
                    return transition.StateTo;

            return null;
        }

        /// <summary>
        /// Adds a method transition to the holder
        /// </summary>
        /// <param name="stateTo">Where the state should transition to</param>
        /// <param name="method">The method that we need to check</param>
        /// <param name="expectedBool">The expected return value of the method</param>
        public void AddMethodTransition(string stateTo, Func<bool> method, bool expectedBool)
        {
            // Init the method transition list if not already created
            if (_methodTransitions == null)
                _methodTransitions = new List<MethodStateTransition>();

            _methodTransitions.Add(new MethodStateTransition(stateTo, method, expectedBool));
        }

        /// <summary>
        /// Checks to see if any of the method transitions are valid
        /// </summary>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        public string CheckMethodTransitions()
        {
            // Only check if there are method transitions
            if (_methodTransitions == null)
                return null;

            // loop through each transition checking if we have a valid transition
            foreach (MethodStateTransition transition in _methodTransitions)
                if (transition.CheckTransition())
                    return transition.StateTo;

            return null;
        }

        /// <summary>
        /// Adds a collision transition to the holder
        /// </summary>
        /// <param name="stateTo">The state to transition to</param>
        /// <param name="collidableType">The type to collide with to transition</param>
        public void AddCollisionTransition(string stateTo, Type collidableType)
        {
            // Create a collision transition list if none exsist
            if (_collisionTransitions == null)
                _collisionTransitions = new List<CollisionTransition>();

            // Add the transition
            _collisionTransitions.Add(new CollisionTransition(stateTo, collidableType));
        }

        /// <summary>
        /// Checks to see if any of the collision transitions are true
        /// </summary>
        /// <param name="eventArgs">The event args to test against</param>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        public string CheckCollisionTransitions(NanoCollisionEventArgs eventArgs)
        {
            // Only check if any transitions exsist
            if (_collisionTransitions == null)
                return null;

            // Loop through each transition and check if any match
            foreach (CollisionTransition transition in _collisionTransitions)
                if (transition.CheckTransition(eventArgs))
                    return transition.StateTo;

            // If we have reached here no tranistions have been met
            return null;
        }
    }
}
