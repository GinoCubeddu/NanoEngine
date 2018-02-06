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
        private static bool created = false;

        //Private event for when a key is pressed
        private event EventHandler<NanoKeyboardEventArgs> OnKeyPressed;
        /// <summary>
        /// Public getter and setter to control the OnKeyPressed event
        /// </summary>
        public EventHandler<NanoKeyboardEventArgs> GetOnKeyPressed
        {
            get { return OnKeyPressed; }
            set { OnKeyPressed = value; }
        }

        //Private event for when a key is held down
        private event EventHandler<NanoKeyboardEventArgs> OnKeyDown;
        /// <summary>
        /// Public getter and setter to control the OnKeyDown event
        /// </summary>
        public EventHandler<NanoKeyboardEventArgs> GetOnKeyDown
        {
            get { return OnKeyDown; }
            set { OnKeyDown = value; }
        }

        //Private event for when a key is released
        private event EventHandler<NanoKeyboardEventArgs> OnKeyReleased;
        /// <summary>
        /// Public getter and setter to control the OnKeyReleased
        /// </summary>
        public EventHandler<NanoKeyboardEventArgs> GetOnKeyReleased
        {
            get { return OnKeyReleased; }
            set { OnKeyReleased = value; }
        }

        //private fields holding the current and previous keyboard states
        private KeyboardState currentKeyState, PrevKeyState;

        public KeyboardHandler()
        {
                created = true;
        }

        /// <summary>
        /// Update method to update current key states and watch for events
        /// </summary>
        public void Update()
        {
            //Make the previous state equal to the current
            PrevKeyState = currentKeyState;
            //Make the current state equal to the keyboard state
            currentKeyState = Keyboard.GetState();
            //If the state has chaged
            if (currentKeyState != PrevKeyState)
            {
                //Create a new list containing the keys that will be sent to the event
                IList<Keys> keysToBeSent = currentKeyState.GetPressedKeys().ToList();
                //If the keys to be sent is not 0
                if (keysToBeSent.Count != 0)
                {
                    //Loop through the current state of keys
                    for (int i = 0; i < currentKeyState.GetPressedKeys().Count(); i++)
                    {
                        //Loop through the previous state of keys
                        for (int j = 0; j < PrevKeyState.GetPressedKeys().Count(); j++)
                        {
                            //If the a key exsists in both states
                            if (currentKeyState.GetPressedKeys()[i] == PrevKeyState.GetPressedKeys()[j])
                                //remove the matching key from the keys to be sent as its still being held down
                                keysToBeSent.Remove(currentKeyState.GetPressedKeys()[i]);
                        }
                    }
                    //Fire an event for keys that only want to be fired once
                    OnKeyboardPressed(keysToBeSent);
                }
            }

            //If any keys are being held down 
            if (currentKeyState.GetPressedKeys().Count() != 0)
                //Fire an event with the current set of keys
                OnKeyboardDown(currentKeyState.GetPressedKeys());

            if (currentKeyState != PrevKeyState)
            {
                //Create a new list containg the keys that will be sent to the event
                IList<Keys> keysToBeSent = PrevKeyState.GetPressedKeys().ToList();
                if (keysToBeSent.Count != 0)
                {
                    //Loop through the last set of keys
                    for (int i = 0; i < PrevKeyState.GetPressedKeys().Count(); i++)
                    {
                        //Loop through the current set of keys
                        for (int j = 0; j < currentKeyState.GetPressedKeys().Count(); j++)
                        {
                            //if the current state does not have the same key as the previous state
                            if (!(currentKeyState.GetPressedKeys().Contains(PrevKeyState.GetPressedKeys()[i])))
                                keysToBeSent.Add(PrevKeyState.GetPressedKeys()[i]);
                        }
                    }
                    OnKeyboardReleased(keysToBeSent);
                }
            }
        }

        /// <summary>
        /// Method that fires an event when a key is pressed
        /// </summary>
        /// <param name="pKeys">The keys that are being pressed</param>
        protected virtual void OnKeyboardPressed(IList<Keys> pKeys)
        {
            IDictionary<KeyStates, IList<Keys>> k = new Dictionary<KeyStates, IList<Keys>>();
            k.Add(KeyStates.Pressed, pKeys);
            if (OnKeyPressed != null)
                OnKeyPressed(this, new NanoKeyboardEventArgs { MyKeys = pKeys, TheKeys = k});
        }

        /// <summary>
        /// Method that fires an event when a key is being held down
        /// </summary>
        /// <param name="pKeys">The keys that are being held down</param>
        protected virtual void OnKeyboardDown(IList<Keys> pKeys)
        {
            if (OnKeyDown != null)
                OnKeyDown(this, new NanoKeyboardEventArgs { MyKeys = pKeys });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pKeys">The keys that have been released</param>
        protected virtual void OnKeyboardReleased(IList<Keys> pKeys)
        {
            if (OnKeyReleased != null)
                OnKeyReleased(this, new NanoKeyboardEventArgs { MyKeys = pKeys });
        }
    }
}
