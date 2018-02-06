using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.StateManagement.States;

namespace NanoEngine.StateManagement.Transitions
{
    internal class TransitionHolder
    {
        // Holds all the keyboard transitions for this state
        private IList<KeyboardStateTransition> _keyboardTransitions;
        
        // Holds all the method transitions for this state
        private IList<MethodStateTransition> _methodTransitions;

        public TransitionHolder()
        {
            _keyboardTransitions = null;
            _methodTransitions = null;
        }

        /// <summary>
        /// Adds a keyboard transition to the holder
        /// </summary>
        /// <param name="stateTo">Where the state should transition to</param>
        /// <param name="keyboardType">The type of keyboard event</param>
        /// <param name="keys">The list of keys to be checked</param>
        public void AddKeyboardTransition(Type stateTo, KeyStates keyboardType, IList<Keys> keys)
        {
            // Init the keyboard transition list if it is not already created
            if (_keyboardTransitions == null)
                _keyboardTransitions = new List<KeyboardStateTransition>();

            _keyboardTransitions.Add(new KeyboardStateTransition(
                stateTo, keyboardType, keys
            ));
        }

        /// <summary>
        /// Checks to see if any of the keyboard transitions are valid
        /// </summary>
        /// <param name="keyboardEvent">The event args given by the handler</param>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        public Type CheckKeyboardTransitions(NanoKeyboardEventArgs keyboardEvent)
        {
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
        public void AddMethodTransition(Type stateTo, Func<bool> method)
        {
            // Init the method transition list if not already created
            if (_methodTransitions == null)
                _methodTransitions = new List<MethodStateTransition>();

                _methodTransitions.Add(new MethodStateTransition(stateTo, method));
        }

        /// <summary>
        /// Checks to see if any of the method transitions are valid
        /// </summary>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        public Type CheckMethodTransitions()
        {
            if (_methodTransitions == null)
                return null;

            // loop through each transition checking if we have a valid transition
            foreach (MethodStateTransition transition in _methodTransitions)
                if (transition.CheckTransition())
                    return transition.StateTo;

            return null;
        }
    }
}
