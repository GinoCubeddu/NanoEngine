using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.StateManagement.States;
using NanoEngine.StateManagement.Transitions;

namespace NanoEngine.StateManagement.StateMachine
{
    public class StateMachine<T> : IStateMachine<T>
    {
        // Dictonary to hold what states the machine has access to
        private IDictionary<string, IState<T>> _avaliableStates;

        // Holds the type of the current state which should be updated
        private string currentState;

        private IDictionary<string, TransitionHolder> _stateTransitions;

        private T _owner;

        public StateMachine(T owner)
        {
            _avaliableStates = new Dictionary<string, IState<T>>();
            _stateTransitions = new Dictionary<string, TransitionHolder>();
            currentState = null;
            _owner = owner;
        }

        /// <summary>
        /// Adds a state to the state machine
        /// </summary>
        /// <param name="state">The state to add</param>
        /// <param name="stateName">The unique name to which the state will be identified</param>
        public void AddState(IState<T> state, string stateName)
        {
            if (_avaliableStates.Count == 0)
            {
                currentState = stateName;
                state.Enter(_owner);
            }
                

            if (!CheckStateExsists(stateName))
                _avaliableStates.Add(stateName, state);
        }

        /// <summary>
        /// Adds a transition between states that relys on keyboard input
        /// </summary>
        /// <param name="wantedState">The State we want the keys to be in</param>
        /// <param name="keysRequired">The keys that should be in the state</param>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        public void AddKeyboardTransition(KeyStates wantedState, IList<Keys> keysRequired, string stateFrom, string stateTo)
        {
            ValidateTransition(stateFrom, stateTo);
            CheckTransitionHandlerExsists(stateFrom);
            _stateTransitions[stateFrom].AddKeyboardTransition(stateTo, wantedState, keysRequired);
        }

        /// <summary>
        /// Adds a transition between states that relys on mouse input
        /// </summary>
        /// <param name="mouseStates">A list containing all the mouse states</param>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        public void AddMouseTransition(IList<MouseStates> mouseStates, string stateFrom, string stateTo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a transition between states that relays on the bool return
        /// value of a method
        /// </summary>
        /// <param name="methodBoolCheck">The method name of the owner</param>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        public void AddMethodCheckTransition(Func<bool> methodBoolCheck, string stateFrom, string stateTo)
        {
            AddMethodCheckTransition(methodBoolCheck, stateFrom, stateTo, true);
        }

        /// <summary>
        /// Adds a transition between states that relays on the bool return
        /// value of a method
        /// </summary>
        /// <param name="methodBoolCheck">The method name of the owner</param>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        /// <param name="expectedOutcome">Wether the method shold return true or false</param>
        public void AddMethodCheckTransition(Func<bool> methodBoolCheck, string stateFrom, string stateTo, bool expectedOutcome)
        {
            ValidateTransition(stateFrom, stateTo);
            CheckTransitionHandlerExsists(stateFrom);
            _stateTransitions[stateFrom].AddMethodTransition(stateTo, methodBoolCheck, expectedOutcome);
        }

        /// <summary>
        /// Adds a transition between states that relys on the state being a success
        /// </summary>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        public void AddSuccessTransition(string stateFrom, string stateTo)
        {
            ValidateTransition(stateFrom, stateTo);
            CheckTransitionHandlerExsists(stateFrom);
            _stateTransitions[stateFrom].SuccessState = stateTo;
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
            CheckSuccessTransition();
            CheckMethodTransition();
            _avaliableStates[currentState].Update(_owner);
        }

        private bool CheckStateExsists(string stateType)
        {
            return _avaliableStates.ContainsKey(stateType);
        }

        private void ValidateTransition(string stateFrom, string stateTo)
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

        private void CheckTransitionHandlerExsists(string stateType)
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

        private void ChangeState(string stateTo)
        {
            if (stateTo != null)
            {
                _avaliableStates[currentState].Exit(_owner);
                currentState = stateTo;
                _avaliableStates[currentState].Enter(_owner);
            }
        }

        private void CheckSuccessTransition()
        {
            // THIS IS TEMP AND SHOULD BE MOVED TO THE TRANSITION HOLDER
            if (_stateTransitions.Keys.Contains(currentState) && _stateTransitions[currentState].SuccessState != null)
                if (_avaliableStates[currentState].IsSuccess)
                    ChangeState(_stateTransitions[currentState].SuccessState);

        }
    }
}
