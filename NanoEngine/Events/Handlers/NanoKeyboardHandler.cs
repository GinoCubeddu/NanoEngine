using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Events.Handlers
{
    public class NanoKeyboardHandler : INanoEventHandler
    {
        // Event handler which will send the event
        private event EventHandler<NanoKeyboardEventArgs> OnKeyboardChanged;

        //private fields holding the current and previous keyboard states
        private KeyboardState _currentKeyState, _prevKeyState;

        /// <summary>
        /// Handles the updating for the handler
        /// </summary>
        /// <param name="updateManager">An instance of the update manager</param>
        public void Update(IUpdateManager updateManager)
        {
            // If nothing is subscribed there is no point in checking
            if (OnKeyboardChanged == null)
                return;

            IDictionary<KeyStates, IList<Keys>> k = new Dictionary<KeyStates, IList<Keys>>();
            //Make the previous state equal to the current
            _prevKeyState = _currentKeyState;
            //Make the current state equal to the keyboard state
            _currentKeyState = Keyboard.GetState();

            //If the state has chaged
            if (_currentKeyState != _prevKeyState)
            {
                IList<Keys> pressedKeys = GetPressedKeys(
                    _currentKeyState.GetPressedKeys(), _prevKeyState.GetPressedKeys()
                );

                IList<Keys> releasedKeys = GetReleasedKeys(
                    _currentKeyState.GetPressedKeys(), _prevKeyState.GetPressedKeys()
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
        /// Subsribes the passed in subscribe to the event handler
        /// </summary>
        /// <param name="subscriber">The subscriber that wants to subsribe</param>
        public void Subscribe(INanoEventSubscribe subscriber)
        {
            // Only desubscribe if the subscriber is of the correct type
            if (subscriber is IKeyboardWanted)
                OnKeyboardChanged += ((IKeyboardWanted)subscriber).OnKeyboardChange;
        }

        /// <summary>
        /// Desubsribes the passed in subscribe from the event handler
        /// </summary>
        /// <param name="subscriber">The subscribe that wants to desubsribe</param>
        public void Desubscribe(INanoEventSubscribe subscriber)
        {
            // Only desubscribe if the subscriber is of the correct type
            if (subscriber is IKeyboardWanted)
                OnKeyboardChanged -= ((IKeyboardWanted)subscriber).OnKeyboardChange;
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
            OnKeyboardChanged?.Invoke(this, new NanoKeyboardEventArgs {TheKeys = pKeys});
        }
    }
}
