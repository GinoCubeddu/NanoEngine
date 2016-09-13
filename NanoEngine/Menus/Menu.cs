using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Managers;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void Draw()
        {
            if(active)
            {
                RenderManager.Manager.StartDraw();
                for(int i = 0; i < menuList.Count; i++)
                {
                    if(i == menuPosition)
                    {
                        RenderManager.Manager.Draw(menuList[i].Texture2, menuList[i].Position, Color.White);
                    }
                    else
                    {
                        RenderManager.Manager.Draw(menuList[i].Texture1, menuList[i].Position, Color.White);
                    }
                }
                RenderManager.Manager.EndDraw();
            }
        }

        /// <summary>
        /// Reciver for the key pressed event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyPressed(object sender, NanoKeyboardEventArgs args)
        {
            if (active)
            {
                if (args.MyKeys.Contains(Keys.PageDown))
                    SoundManager.Manager.ChangeLoopedSoundVolume(-0.1F);
                else if (args.MyKeys.Contains(Keys.PageUp))
                    SoundManager.Manager.ChangeLoopedSoundVolume(0.1F);

                SoundManager.Manager.PlaySound(sound);
                if (args.MyKeys.Contains(Keys.Up))
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
                else if (args.MyKeys.Contains(Keys.Down))
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
                else if(args.MyKeys.Contains(Keys.Enter))
                {
                    menuList[menuPosition].Controler.Clicked();
                }
            }
        }

        /// <summary>
        /// Reciver for the key Released event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyReleased(object sender, NanoKeyboardEventArgs args)
        {

        }

        /// <summary>
        /// Reciver for the key Down event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyDown(object sender, NanoKeyboardEventArgs args)
        {

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
    }
}
