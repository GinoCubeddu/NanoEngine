﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Control;
using System.Collections.Generic;
using System;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Menus
{
    public class Menu : IKeyboardWanted, IMouseWanted
    {
        private IList<IMenuItem> menuList;

        private int menuPosition;

        private bool active;

        private SoundEffect sound = ContentManagerLoad.Manager.LoadResource<SoundEffect>("Sounds/rollover3.wav");

        public Menu(IList<IMenuItem> list, bool active)
        {
            menuList = list;
            this.active = active;
        }

        public void Draw(IRenderManager renderManager)
        {
            if(active)
            {                
                for(int i = 0; i < menuList.Count; i++)
                {
                    if(i == menuPosition)
                    {
                        renderManager.Draw(menuList[i].Texture2, menuList[i].Position, Color.White);
                    }
                    else
                    {
                        renderManager.Draw(menuList[i].Texture1, menuList[i].Position, Color.White);
                    }
                }
            }
        }

        /// <summary>
        /// Event Reciver for the mouse down event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseDown(object sender, NanoMouseEventArgs e)
        {

        }

        /// <summary>
        /// Event Reciver for mouse up event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseUp(object sender, NanoMouseReleasedArgs e)
        {
            if(active)
            {
                if(e.Left)
                {
                    Rectangle rect = new Rectangle((int)menuList[menuPosition].Position.X, (int)menuList[menuPosition].Position.Y, (int)menuList[menuPosition].Texture1.Width, (int)menuList[menuPosition].Texture1.Height);
                    if(rect.Contains(e.Position))
                    {
                        menuList[menuPosition].Controler.Clicked();
                    }
                }
            }
        }

        /// <summary>
        /// Event Reciver for mouse moved event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseMoved(object sender, NanoMouseEventArgs e)
        {
            if(active)
            {
                for(int i = 0; i < menuList.Count; i++)
                {
                    Rectangle rect = new Rectangle((int)menuList[i].Position.X, (int)menuList[i].Position.Y, (int)menuList[i].Texture1.Width, (int)menuList[i].Texture1.Height);
                    if(rect.Contains(e.CurrentMouseState.Position))
                    {
                        if (menuPosition != i)
                            SoundManager.Manager.PlaySound(sound);

                        menuPosition = i;
                    }
                }
            }
        }

        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            if (active)
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.PageDown))
                    SoundManager.Manager.ChangeLoopedSoundVolume(-0.1F);
                else if (args.TheKeys[KeyStates.Pressed].Contains(Keys.PageUp))
                    SoundManager.Manager.ChangeLoopedSoundVolume(0.1F);

                SoundManager.Manager.PlaySound(sound);
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Up))
                {
                    if (menuPosition == 0)
                    {
                        menuPosition = menuList.Count - 1;
                    }
                    else
                    {
                        menuPosition--;
                    }
                }
                else if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Down))
                {
                    if (menuPosition == menuList.Count - 1)
                    {
                        menuPosition = 0;
                    }
                    else
                    {
                        menuPosition++;
                    }
                }
                else if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Enter))
                {
                    menuList[menuPosition].Controler.Clicked();
                }
            }
        }
    }
}
