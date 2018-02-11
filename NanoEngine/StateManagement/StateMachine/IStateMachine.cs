using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.StateManagement.States;

namespace NanoEngine
{
    public interface IStateMachine
    {
        /// <summary>
        /// Adds a state to the state machine
        /// </summary>
        /// <param name="state">The state to add</param>
        /// <param name="stateName">The unique name to which the state will be identified</param>
        void AddState(IState state, string stateName);

        /// <summary>
        /// Adds a transition between states that relys on keyboard input
        /// </summary>
        /// <param name="wantedState">The State we want the keys to be in</param>
        /// <param name="keysRequired">The keys that should be in the state</param>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        void AddKeyboardTransition(KeyStates wantedState, IList<Keys> keysRequired, string stateFrom, string stateTo);

        /// <summary>
        /// Adds a transition between states that relys on mouse input
        /// </summary>
        /// <param name="mouseStates">A list containing all the mouse states</param>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        void AddMouseTransition(IList<MouseStates> mouseStates, string stateFrom, string stateTo);

        /// <summary>
        /// Adds a transition between states that relays on the bool return
        /// value of a method
        /// </summary>
        /// <param name="methodBoolCheck">The method name of the owner</param>
        /// <param name="stateFrom">The sate to transition from</param>
        /// <param name="stateTo">The state to transition to</param>
        void AddMethodCheckTransition(Func<bool> methodBoolCheck, string stateFrom, string stateTo);

        /// <summary>
        /// Allows the state machine to handle any collision events that may
        /// cause the state to change
        /// </summary>
        /// <param name="collisionArgs">Arguments that contain information on the event</param>
        void HandleCollision(NanoMouseEventArgs collisionArgs);

        /// <summary>
        /// Allows the state machine to handle any keyboard events that may
        /// cause the state to change
        /// </summary>
        /// <param name="keyboardArgs">Arguments that contain information on the event</param>
        void HandleKeyboardInput(NanoKeyboardEventArgs keyboardArgs);

        /// <summary>
        /// Allows the state machine to handle any mouse events that may
        /// cause the state to change
        /// </summary>
        /// <param name="mouseArgs">Arguments that contain information on the event</param>
        void HandleMouseInput(NanoMouseEventArgs mouseArgs);

        /// <summary>
        /// Updates the currently active state
        /// </summary>
        void Update();
    }
}
