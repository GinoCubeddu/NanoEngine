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

        // Holds all the transitions for a specfic state
        private IDictionary<string, ITransitionHolder> _stateTransitions;

        // Holds the owner of teh states
        private T _owner;

        public StateMachine(T owner)
        {
            _avaliableStates = new Dictionary<string, IState<T>>();
            _stateTransitions = new Dictionary<string, ITransitionHolder>();
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
            // If no states exsist then this si the first state
            if (_avaliableStates.Count == 0)
            {
                currentState = stateName;
                state.Enter(_owner, null);
            }
                
            // Only add the state if it does not already exsist
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
        /// Adds a transition between states that relys on the asset colliding with a
        /// certian type
        /// </summary>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        /// <param name="collidableType">The type to collide with to transition</param>
        public void AddCollisionTransition(string stateFrom, string stateTo, Type collidableType)
        {
            ValidateTransition(stateFrom, stateTo);
            CheckTransitionHandlerExsists(stateFrom);
            _stateTransitions[stateFrom].AddCollisionTransition(stateTo, collidableType);
        }

        /// <summary>
        /// Allows the state machine to handle any collision events that may
        /// cause the state to change
        /// </summary>
        /// <param name="collisionArgs">Arguments that contain information on the event</param>
        /// <param name="stateArguments">Holds any arguments the mind has passed to the statemachine</param>
        public void HandleCollision(NanoCollisionEventArgs collisionArgs, IDictionary<string, object> stateArguments)
        {
            if (_stateTransitions.Keys.Contains(currentState))
            {
                ChangeState(_stateTransitions[currentState].CheckCollisionTransitions(collisionArgs), stateArguments);
            }
        }

        /// <summary>
        /// Allows the state machine to handle any collision events that may
        /// cause the state to change
        /// </summary>
        /// <param name="collisionArgs">Arguments that contain information on the event</param>
        public void HandleCollision(NanoCollisionEventArgs collisionArgs)
        {
            HandleCollision(collisionArgs, null);
        }

        /// <summary>
        /// Allows the state machine to handle any keyboard events that may
        /// cause the state to change
        /// </summary>
        /// <param name="keyboardArgs">Arguments that contain information on the event</param>
        /// <param name="stateArguments">Holds any arguments the mind has passed to the statemachine</param>
        public void HandleKeyboardInput(NanoKeyboardEventArgs keyboardArgs, IDictionary<string, object> stateArguments)
        {
            if (_stateTransitions.Keys.Contains(currentState))
            {
                ChangeState(_stateTransitions[currentState].CheckKeyboardTransitions(keyboardArgs), stateArguments);
            }
        }

        /// <summary>
        /// Allows the state machine to handle any keyboard events that may
        /// cause the state to change
        /// </summary>
        /// <param name="keyboardArgs">Arguments that contain information on the event</param>
        public void HandleKeyboardInput(NanoKeyboardEventArgs keyboardArgs)
        {
            HandleKeyboardInput(keyboardArgs, null);
        }

        /// <summary>
        /// Allows the state machine to handle any mouse events that may
        /// cause the state to change
        /// </summary>
        /// <param name="mouseArgs">Arguments that contain information on the event</param>
        /// <param name="stateArguments">Holds any arguments the mind has passed to the statemachine</param>
        public void HandleMouseInput(NanoMouseEventArgs mouseArgs, IDictionary<string, object> stateArguments)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows the state machine to handle any mouse events that may
        /// cause the state to change
        /// </summary>
        /// <param name="mouseArgs">Arguments that contain information on the event</param>
        public void HandleMouseInput(NanoMouseEventArgs mouseArgs)
        {
            HandleMouseInput(mouseArgs, null);
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

        /// <summary>
        /// Check to see if the state already exsists
        /// </summary>
        /// <param name="stateType">The name of the state</param>
        /// <returns>True if the state exsists false if not</returns>
        private bool CheckStateExsists(string stateType)
        {
            return _avaliableStates.ContainsKey(stateType);
        }

        /// <summary>
        /// Validates if the transistion is a valid one
        /// </summary>
        /// <param name="stateFrom">The name of the state to transition from</param>
        /// <param name="stateTo">The name of the state to transition to</param>
        private void ValidateTransition(string stateFrom, string stateTo)
        {
            // Throw an error if either of the states do not exsist
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

            // Throw exception if trying to transition to itsself
            if (stateFrom == stateTo)
                throw new Exception("Unable to transition state to itsself");
        }

        /// <summary>
        /// Checks to see if the transition handler exsists for the passed in
        /// state type if one does not then it is created
        /// </summary>
        /// <param name="stateType">The name of the state</param>
        private void CheckTransitionHandlerExsists(string stateType)
        {
            if(!_stateTransitions.ContainsKey(stateType))
                _stateTransitions.Add(stateType, new TransitionHolder());
        }

        /// <summary>
        /// Checks if the state transition for the method call is ready
        /// If it is ready to transition then switch out the state to
        /// the new one
        /// </summary>
        private void CheckMethodTransition()
        {
            if (_stateTransitions.Keys.Contains(currentState))
                ChangeState(_stateTransitions[currentState].CheckMethodTransitions(), null);
        }

        /// <summary>
        /// Changes the current state to the passed in state
        /// </summary>
        /// <param name="stateTo"></param>
        /// <param name="stateArguments">Holds any arguments the mind has passed to the statemachine</param>
        private void ChangeState(string stateTo, IDictionary<string, object> stateArguments)
        {
            // Only change the state if the passed in state is not null
            if (stateTo != null)
            {
                _avaliableStates[currentState].Exit(_owner);
                currentState = stateTo;
                _avaliableStates[currentState].Enter(_owner, stateArguments);
            }
        }

        private void CheckSuccessTransition()
        {
            // THIS IS TEMP AND SHOULD BE MOVED TO THE TRANSITION HOLDER
            if (_stateTransitions.Keys.Contains(currentState) && _stateTransitions[currentState].SuccessState != null)
                if (_avaliableStates[currentState].IsSuccess)
                    ChangeState(_stateTransitions[currentState].SuccessState, null);

        }
    }
}
