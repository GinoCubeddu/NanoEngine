﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Control;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Locator;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Menus
{
    public class MenuOld : IKeyboardWanted, IMouseWanted
    {
        // NOTE: THIS CLASS IS PENDING REFACTOR AND AS SUCH WILL BE COMMENTED ON REFACTOR

        

        private IList<IMenuItem> menuList;

        private int menuPosition;

        private string _soundEffect;

        private bool active;

        public MenuOld(IList<IMenuItem> list, bool active, string soundEffectname)
        {
            menuList = list;
            this.active = active;
            _soundEffect = soundEffectname;
        }

        public MenuOld(IList<IMenuItem> list, bool active) : this(list, active, null)
        {
            
        }

        public void Draw(IRenderManager renderManager)
        {
            if(active)
            {                
                for(int i = 0; i < menuList.Count; i++)
                {
                    if(i == menuPosition)
                    {
                        renderManager.Draw(menuList[i].Texture2, menuList[i].Position, null, Color.White, 0,
                            Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
                    }
                    else
                    {
                        renderManager.Draw(menuList[i].Texture1, menuList[i].Position, null, Color.White, 0,
                            Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
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
                        if (menuPosition != i && _soundEffect != null)
                            ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager).PlayBaseSoundEffect(_soundEffect);

                        menuPosition = i;
                    }
                }
            }
        }

        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            if (active)
            {
                if (!args.TheKeys.Keys.Contains(KeyStates.Pressed))
                    return;

                if (_soundEffect != null)
                    ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager).PlaySoundEffect(_soundEffect);

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
            }
        }

        /// <summary>
        /// Event Reciver for the mouse down event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseChanged(object sender, NanoMouseEventArgs e)
        {
            // throw new NotImplementedException();
            // if (e.CurrentMouseState.)
            for (int i = 0; i < menuList.Count; i++)
            {
                Rectangle rect = new Rectangle((int)menuList[i].Position.X, (int)menuList[i].Position.Y, (int)menuList[i].Texture1.Width, (int)menuList[i].Texture1.Height);
                if (rect.Contains(e.CurrentMouseState.Position))
                {
                    if (menuPosition != i && _soundEffect != null)
                        ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager).PlayBaseSoundEffect(_soundEffect);

                    menuPosition = i;
                }
            }

            if (e.CurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle rect = new Rectangle((int)menuList[menuPosition].Position.X, (int)menuList[menuPosition].Position.Y, (int)menuList[menuPosition].Texture1.Width, (int)menuList[menuPosition].Texture1.Height);
                if (rect.Contains(e.CurrentMouseState.Position))
                {
                    menuList[menuPosition].Controler.Clicked();
                }
            }  
        }
    }
}