using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Events.Handlers
{
    public class KeyboardHandler : IKeyboardHandler
    {
        private event EventHandler<NanoKeyboardEventArgs> _onKeyboardChanged;

        public EventHandler<NanoKeyboardEventArgs> GetOnKeyboardChanged
        {
            get { return _onKeyboardChanged; }
            set { _onKeyboardChanged = value; }
        }

        //private fields holding the current and previous keyboard states
        private KeyboardState currentKeyState, PrevKeyState;

        public KeyboardHandler()
        {
        }

        /// <summary>
        /// Update method to update current key states and watch for events
        /// </summary>
        public void Update()
        {
            IDictionary<KeyStates, IList<Keys>> k = new Dictionary<KeyStates, IList<Keys>>();
            //Make the previous state equal to the current
            PrevKeyState = currentKeyState;
            //Make the current state equal to the keyboard state
            currentKeyState = Keyboard.GetState();

            //If the state has chaged
            if (currentKeyState != PrevKeyState)
            {
                IList<Keys> pressedKeys = GetPressedKeys(
                    currentKeyState.GetPressedKeys(), PrevKeyState.GetPressedKeys()
                );

                IList<Keys> releasedKeys = GetReleasedKeys(
                    currentKeyState.GetPressedKeys(), PrevKeyState.GetPressedKeys()
                );                

                if (pressedKeys.Count > 0)
                    k.Add(KeyStates.Pressed, pressedKeys);
                                         
                if (releasedKeys.Count > 0)
                    k.Add(KeyStates.Released, releasedKeys);                
            }

            if (k.Keys.Count > 0)
                KeyboardChange(k);
        }

        /// <summary>
        /// Returns a list containing all the released keys
        /// </summary>
        /// <param name="currentKeys">All the current keys that are pressed</param>
        /// <param name="previousKeys">All the previous keys that are pressed</param>
        /// <returns>The list containing the released keys</returns>
        private IList<Keys> GetReleasedKeys(IList<Keys> currentKeys, IList<Keys> previousKeys)
        {
            IList<Keys> releasedKeys = new List<Keys>();
            foreach (Keys key in previousKeys)
            {
                if (!currentKeys.Contains(key))
                    releasedKeys.Add(key);
            }
            return releasedKeys;
        }

        /// <summary>
        /// Returns a list containing all the newly pressed keys
        /// </summary>
        /// <param name="currentKeys">All the current keys that are pressed</param>
        /// <param name="previousKeys">All the previous keys that are pressed</param>
        /// <returns>The list containing the newly pressed keys</returns>
        private IList<Keys> GetPressedKeys(IList<Keys> currentKeys, IList<Keys> previousKeys)
        {
            IList<Keys> pressedKeys = new List<Keys>(currentKeys);
            // Loop through all the previous keys
            foreach (Keys key in previousKeys)
            {
                // if the previous key is in the current keys then don't resend the event
                // and remove it from the keys that will be sent to the PRESSED event
                if (currentKeys.Contains(key))
                    pressedKeys.Remove(key);
            }
            return pressedKeys;
        }

        /// <summary>
        /// Sends an event when the keyboard state chanages
        /// </summary>
        /// <param name="pKeys">A dict conatining the keys and what state they are in</param>
        protected virtual void KeyboardChange(IDictionary<KeyStates, IList<Keys>> pKeys)
        {
            _onKeyboardChanged(this, new NanoKeyboardEventArgs { TheKeys = pKeys });
        }
    }
}
