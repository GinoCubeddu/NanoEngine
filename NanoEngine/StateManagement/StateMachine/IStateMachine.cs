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
        /// <typeparam name="T">The type of state to transition from</typeparam>
        /// <typeparam name="U">The type of state to transition to</typeparam>
        /// <param name="wantedState">The State we want the keys to be in</param>
        /// <param name="keysRequired">The keys that should be in the state</param>
        void AddKeyboardTransition<T, U>(KeyStates wantedState, IList<Keys> keysRequired) 
            where T : IState
            where U : IState;

        /// <summary>
        /// Adds a transition between states that relys on mouse input
        /// </summary>
        /// <typeparam name="T">The type of state to transition from</typeparam>
        /// <typeparam name="U">The type of state to transition to</typeparam>
        /// <param name="mouseStates">A list containing all the mouse states</param>
        void AddMouseTransition<T, U>(IList<MouseStates> mouseStates)
            where T : IState
            where U : IState;

        /// <summary>
        /// Adds a transition between states that relays on the bool return
        /// value of a method
        /// </summary>
        /// <typeparam name="T">The type of state to transition from</typeparam>
        /// <typeparam name="U">The type of state to transition to</typeparam>
        /// <param name="methodBoolCheck">The method name of the owner</param>
        void AddMethodCheckTransition<T, U>(Func<bool> methodBoolCheck)
            where T : IState
            where U : IState;

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
