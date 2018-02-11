using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.StateManagement.States;

namespace NanoEngine.StateManagement.Transitions
{
    internal class KeyboardStateTransition
    {
        // Holds the type of which state to transition to
        public Type StateTo { get; }

        // Holds which key state we want to check for
        private KeyStates _keyboardTransition;

        // Holds all the keys required be be in the keystate for transistion to be true
        private IList<Keys> _keys;

        /// <summary>
        /// Constructor for the keyboard transition
        /// </summary>
        /// <param name="type">The key state we are checking the keys against</param>
        /// <param name="keysRequired">The keys required to be in the key state</param>
        /// <param name="stateTo">The statebehaviour we want to transition to</param>
        public KeyboardStateTransition(Type stateTo, KeyStates type, IList<Keys> keysRequired)
        {
            _keyboardTransition = type;
            _keys = keysRequired;
            StateTo = stateTo;
        }

        /// <summary>
        /// Method that checks if the transition is valid
        /// </summary>
        /// <param name="eventArgs">event args holding the nessecery incofmation</param>
        /// <returns>Boolean telling us if it is a valid transition</returns>
        public bool CheckTransition(NanoKeyboardEventArgs eventArgs)
        {
            if (eventArgs.TheKeys.Keys.Contains(_keyboardTransition))
             return _keys.All(key => eventArgs.TheKeys[_keyboardTransition].Contains(key));
            return false;
        }
    }
}
