using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.StateManagement.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.StateManagement.Transitions
{
    public interface ITransitionHolder
    {
        // Holds the state for the state to transition to if it is a success
        string SuccessState { get; set; }

        /// <summary>
        /// Adds a keyboard transition to the holder
        /// </summary>
        /// <param name="stateTo">Where the state should transition to</param>
        /// <param name="keyboardType">The type of keyboard event</param>
        /// <param name="keys">The list of keys to be checked</param>
        void AddKeyboardTransition(string stateTo, KeyStates keyboardType, IList<Keys> keys);

        /// <summary>
        /// Checks to see if any of the keyboard transitions are valid
        /// </summary>
        /// <param name="keyboardEvent">The event args given by the handler</param>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        string CheckKeyboardTransitions(NanoKeyboardEventArgs keyboardEvent);

        /// <summary>
        /// Adds a method transition to the holder
        /// </summary>
        /// <param name="stateTo">Where the state should transition to</param>
        /// <param name="method">The method that we need to check</param>
        /// <param name="expectedBool">The expected return value of the method</param>
        void AddMethodTransition(string stateTo, Func<bool> method, bool expectedBool);

        /// <summary>
        /// Checks to see if any of the method transitions are valid
        /// </summary>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        string CheckMethodTransitions();

        /// <summary>
        /// Adds a collision transition to the holder
        /// </summary>
        /// <param name="stateTo">The state to transition to</param>
        /// <param name="collidableType">The type to collide with to transition</param>
        void AddCollisionTransition(string stateTo, Type collidableType);

        /// <summary>
        /// Checks to see if any of the collision transitions are true
        /// </summary>
        /// <param name="eventArgs">The event args to test against</param>
        /// <returns>The type of the next state if transition requirements met otherwise null</returns>
        string CheckCollisionTransitions(NanoCollisionEventArgs eventArgs);
    }
}
