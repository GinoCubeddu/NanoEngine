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
        void AddState(IState state);

        /// <summary>
        /// Adds a transition between states that relys on keyboard input
        /// </summary>
        /// <param name="stateFrom">The type of state to transition from</param>
        /// <param name="stateTo">The type of state to transition to</param>
        /// <param name="keyStates">
        /// A dictonary containing a keyboard transationtype as it's key
        /// and a list of Keys and its value
        /// </param>
        void AddKeyboardTransition(
            Type stateFrom, Type stateTo,
            IDictionary<KeyStates, IList<Keys>> keyStates
        );

        /// <summary>
        /// Adds a transition between states that relys on mouse input
        /// </summary>
        /// <param name="stateFrom">The type of state to transition from</param>
        /// <param name="stateTo">The type of state to transition to</param>
        /// <param name="mouseStates">A list containing all the mouse states</param>
        void AddMouseTransition(
            Type stateFrom, Type stateTo, IList<MouseStates> mouseStates
        );

        /// <summary>
        /// Adds a transition between states that relays on the bool return
        /// value of a method
        /// </summary>
        /// <param name="stateFrom">The type of state to transition from</param>
        /// <param name="stateTo">The type of state to transition to</param>
        /// <param name="methodBoolCheck">The method name of the owner</param>
        void AddMethodCheckTransition(
            Type stateFrom, Type stateTo, Func<bool> methodBoolCheck
        );

        /// <summary>
        /// Allows the state machine to handle any collision events that may
        /// cause the state to change
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        /// <param name="collisionArgs">Arguments that contain information on the event</param>
        void HandleCollision<T>(T owner, NanoMouseEventArgs collisionArgs);

        /// <summary>
        /// Allows the state machine to handle any keyboard events that may
        /// cause the state to change
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        /// <param name="keyboardArgs">Arguments that contain information on the event</param>
        void HandleKeyboardInput<T>(T owner, NanoKeyboardEventArgs keyboardArgs);

        /// <summary>
        /// Allows the state machine to handle any mouse events that may
        /// cause the state to change
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        /// <param name="mouseArgs">Arguments that contain information on the event</param>
        void HandleMouseInput<T>(T owner, NanoMouseEventArgs mouseArgs);

        /// <summary>
        /// Updates the currently active state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        void Update<T>(T owner);
    }
}
