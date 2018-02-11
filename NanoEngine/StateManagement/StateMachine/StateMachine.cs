using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.StateManagement.States;
using NanoEngine.StateManagement.Transitions;

namespace NanoEngine.StateManagement.StateMachine
{
    public class StateMachine<T> : IStateMachine
    {
        // Dictonary to hold what states the machine has access to
        private IDictionary<Type, IState> _avaliableStates;

        // Holds the type of the current state which should be updated
        private Type currentState;

        private IDictionary<Type, TransitionHolder> _stateTransitions;

        private T _owner;

        public StateMachine(T owner)
        {
            _avaliableStates = new Dictionary<Type, IState>();
            _stateTransitions = new Dictionary<Type, TransitionHolder>();
            currentState = null;
            _owner = owner;
        }

        /// <summary>
        /// Adds a state to the state machine
        /// </summary>
        /// <param name="state">The state to add</param>
        public void AddState(IState state)
        {
            if (_avaliableStates.Count == 0)
            {
                currentState = state.GetType();
                state.Enter(_owner);
            }
                

            if (!CheckStateExsists(state.GetType()))
                _avaliableStates.Add(state.GetType(), state);
        }

        /// <summary>
        /// Adds a transition between states that relys on keyboard input
        /// </summary>
        /// <typeparam name="T">The type of state to transition from</typeparam>
        /// <typeparam name="U">The type of state to transition to</typeparam>
        /// <param name="wantedState">The State we want the keys to be in</param>
        /// <param name="keysRequired">The keys that should be in the state</param>
        public void AddKeyboardTransition<T, U>(KeyStates wantedState, IList<Keys> keysRequired)
            where T : IState
            where U : IState
        {
            ValidateTransition(typeof(T), typeof(U));
            CheckTransitionHandlerExsists(typeof(T));
            _stateTransitions[typeof(T)].AddKeyboardTransition(typeof(U), wantedState, keysRequired);
        }

        /// <summary>
        /// Adds a transition between states that relys on mouse input
        /// </summary>
        /// <typeparam name="T">The type of state to transition from</typeparam>
        /// <typeparam name="U">The type of state to transition to</typeparam>
        /// <param name="mouseStates">A list containing all the mouse states</param>
        public void AddMouseTransition<T, U>(IList<MouseStates> mouseStates)
            where T : IState
            where U : IState
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a transition between states that relays on the bool return
        /// value of a method
        /// </summary>
        /// <typeparam name="T">The type of state to transition from</typeparam>
        /// <typeparam name="U">The type of state to transition to</typeparam>
        /// <param name="methodBoolCheck">The method name of the owner</param>
        public void AddMethodCheckTransition<T, U>(Func<bool> methodBoolCheck)
            where T : IState
            where U : IState
        {
            ValidateTransition(typeof(T), typeof(U));
            CheckTransitionHandlerExsists(typeof(T));
            _stateTransitions[typeof(T)].AddMethodTransition(typeof(U), methodBoolCheck);
        }

        /// <summary>
        /// Allows the state machine to handle any collision events that may
        /// cause the state to change
        /// </summary>
        /// <param name="collisionArgs">Arguments that contain information on the event</param>
        public void HandleCollision(NanoMouseEventArgs collisionArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows the state machine to handle any keyboard events that may
        /// cause the state to change
        /// </summary>
        /// <param name="keyboardArgs">Arguments that contain information on the event</param>
        public void HandleKeyboardInput(NanoKeyboardEventArgs keyboardArgs)
        {
            if (_stateTransitions.Keys.Contains(currentState))
            {
                ChangeState(_stateTransitions[currentState].CheckKeyboardTransitions(keyboardArgs));
            }
        }

        /// <summary>
        /// Allows the state machine to handle any mouse events that may
        /// cause the state to change
        /// </summary>
        /// <param name="mouseArgs">Arguments that contain information on the event</param>
        public void HandleMouseInput(NanoMouseEventArgs mouseArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the currently active state
        /// </summary>
        public void Update()
        {
            CheckMethodTransition();
            _avaliableStates[currentState].Update(_owner);
        }

        private bool CheckStateExsists(Type stateType)
        {
            return _avaliableStates.ContainsKey(stateType);
        }

        private void ValidateTransition(Type stateFrom, Type stateTo)
        {
            if (!CheckStateExsists(stateFrom))
                throw new Exception(String.Format(
                    "Unable to create transition from {0} to {1} as state {0} " + 
                    "has not been added to the state machine",
                    stateFrom.GetType().ToString(),
                    stateTo.GetType().ToString()
                ));

            if (!CheckStateExsists(stateTo))
                throw new Exception(String.Format(
                    "Unable to create transition from {0} to {1} as state {1} " +
                    "has not been added to the state machine",
                    stateFrom.GetType().ToString(),
                    stateTo.GetType().ToString()
                ));
            Console.WriteLine(stateFrom);
            Console.WriteLine(stateTo);

            if (stateFrom == stateTo)
                throw new Exception("Unable to transition state to itsself");
        }

        private void CheckTransitionHandlerExsists(Type stateType)
        {
            if(!_stateTransitions.ContainsKey(stateType))
                _stateTransitions.Add(stateType, new TransitionHolder());
        }

        private void CheckMethodTransition()
        {
            if (_stateTransitions.Keys.Contains(currentState))
            {
                ChangeState(_stateTransitions[currentState].CheckMethodTransitions());
            }
        }

        private void ChangeState(Type stateTo)
        {
            if (stateTo != null)
            {
                _avaliableStates[currentState].Exit(_owner);
                currentState = stateTo;
                _avaliableStates[currentState].Enter(_owner);
            }
        }
    }
}
