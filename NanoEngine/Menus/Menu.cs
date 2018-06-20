using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Menus
{
    class Menu : IMenu, IKeyboardWanted, IMouseWanted
    {
        // The name of the menu
        public string Name { get; }

        // A list to hold the menu buttons
        private IList<IButton> _menuButtons;

        // The key used to activate the menu
        private Keys _activatorKey;

        // The current button that is selected
        private int _currentButton;

        private string _soundEffect;

        public Menu(string name)
        {
            Name = name;
            _menuButtons = new List<IButton>();
            _activatorKey = Keys.Enter;
            _currentButton = 0;
        }

        /// <summary>
        /// Adds a button to the menu
        /// </summary>
        /// <param name="button"></param>
        public void AddButton(IButton button)
        {
            _menuButtons.Add(button);
            if (_menuButtons.Count > 1)
                button.ToggleActiveTexture();
        }

        /// <summary>
        /// Draws the buttons within the menu
        /// </summary>
        /// <param name="renderManager">An instance of the render manager</param>
        public void Draw(IRenderManager renderManager)
        {
            foreach (IButton menuButton in _menuButtons)
                menuButton.Draw(renderManager);
        }

        /// <summary>
        /// Sets the key to activate the button
        /// </summary>
        /// <param name="key">The activator</param>
        public void SetActivateButton(Keys key)
        {
            _activatorKey = key;
        }

        /// <summary>
        /// Sets the sound effect for the menu
        /// </summary>
        /// <param name="soundEffect">The name of the sound effect</param>
        public void SetSoundEffect(string soundEffect)
        {
            _soundEffect = soundEffect;
        }

        /// <summary>
        /// Reciver for the keyboard change event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            // If a button was pressed
            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                // If it was the activator key then activate the button
                if (args.TheKeys[KeyStates.Pressed].Contains(_activatorKey))
                    _menuButtons[_currentButton].Activate();

                // If it was the down arrow move down the list
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Down))
                {
                    // Toggle the texture of the current button
                    _menuButtons[_currentButton].ToggleActiveTexture();

                    // Change the current button to one higher unless its already at the max button then set to 0
                    _currentButton = _currentButton == _menuButtons.Count - 1 ? 0 : _currentButton + 1;

                    // Toggle the texture of the new button
                    _menuButtons[_currentButton].ToggleActiveTexture();

                    // Play the sound effect top notify the change
                    PlaySoundEffect();
                }

                // If it was the down arrow move up the list
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Up))
                {
                    // Toggle the texture of the current button
                    _menuButtons[_currentButton].ToggleActiveTexture();

                    // Change the current button to one lower unless its already at the first button then set to the last
                    _currentButton = _currentButton == 0 ? _menuButtons.Count - 1 : _currentButton - 1;

                    // Toggle the texture of the new button
                    _menuButtons[_currentButton].ToggleActiveTexture();

                    // Play the sound effect to notify the change
                    PlaySoundEffect();
                }
            }
        }

        /// <summary>
        /// Event Reciver for the mouse down event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseChanged(object sender, NanoMouseEventArgs e)
        {
            // loop through each button that the menu has
            for (int i = 0; i < _menuButtons.Count; i++)
            {
                // Create a rect out of the dimentions and position of the texture
                Rectangle rect = new Rectangle(
                    (int)_menuButtons[i].Position.X,
                    (int)_menuButtons[i].Position.Y,
                    (int)_menuButtons[i].CurrentTexture.Width,
                    (int)_menuButtons[i].CurrentTexture.Height
                );

                // If the created rectangle contains the mouse then proceed
                if (rect.Contains(e.CurrentMouseState.Position))
                {
                    // If the mouse is on a different one to the selected one play the sound effect and change the selectedc btn
                    if (_currentButton != i)
                    {
                        // change the texture of the current one
                        _menuButtons[_currentButton].ToggleActiveTexture();
                        _currentButton = i;

                        // Toggle the texture of the new button
                        _menuButtons[_currentButton].ToggleActiveTexture();

                        // Play the sound effect to notify the change
                        PlaySoundEffect();
                    }
                }
            }

            // If the mouse was pressed
            if (e.CurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                // Create a rect out of the dimentions and position of the texture
                Rectangle rect = new Rectangle(
                    (int)_menuButtons[_currentButton].Position.X,
                    (int)_menuButtons[_currentButton].Position.Y,
                    (int)_menuButtons[_currentButton].CurrentTexture.Width,
                    (int)_menuButtons[_currentButton].CurrentTexture.Height
                );

                // If the mouse was within the selected button activate it
                if (rect.Contains(e.CurrentMouseState.Position))
                {
                    _menuButtons[_currentButton].Activate();
                }
            }
        }

        /// <summary>
        /// Plays the sound effect if one has been set
        /// </summary>
        private void PlaySoundEffect()
        {
            if (_soundEffect != null)
                ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager)
                    .PlayBaseSoundEffect(_soundEffect);
        }
    }
}
