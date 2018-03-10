using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events.Handlers
{
    public class MouseHandler : IMouseHandler
    {
        //Two states to hold the current and the previous recored states
        MouseState currentMouseState, previousMouseState;

        //Event to handle when the mouse has been clicked
        private event EventHandler<NanoMouseEventArgs> onMouseDown;
        /// <summary>
        /// Getter/Setter to return the event when the mouse is being clicked
        /// </summary>
        public EventHandler<NanoMouseEventArgs> GetOnMouseDown
        {
            get { return onMouseDown; }
            set { onMouseDown = value; }
        }

        //Event to handle when the mouse is no longer being clicked
        private event EventHandler<NanoMouseReleasedArgs> onMouseUp;
        /// <summary>
        /// Getter/Setter to return the event when the mouse is no longer being clicked
        /// </summary>
        public EventHandler<NanoMouseReleasedArgs> GetOnMouseUp
        {
            get { return onMouseUp; }
            set { onMouseUp = value; }
        }

        //Event to handle when the mouse has been moved
        private event EventHandler<NanoMouseEventArgs> onMouseMoved;

        /// <summary>
        /// Getter/Setter to return the event when the mouse has been moved
        /// </summary>
        public EventHandler<NanoMouseEventArgs> GetOnMouseMoved
        {
            get { return onMouseMoved; }
            set { onMouseMoved = value; }
        }

        public MouseHandler()
        {

        }

        public void Update()
        {
            // There is no point in checking the input if nothing is subsribed
            if (onMouseDown == null)
                return;
            //Set the previous state to the current state
            previousMouseState = currentMouseState;
            //Set the current state to the mouse state
            currentMouseState = Mouse.GetState();
            //Check if any button has been clicked 
            if ((previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed) || (previousMouseState.RightButton == ButtonState.Released && currentMouseState.RightButton == ButtonState.Pressed) || (previousMouseState.MiddleButton == ButtonState.Released && currentMouseState.MiddleButton == ButtonState.Pressed))
            {
                //Fire event
                MouseClicked(currentMouseState);
            }

            //Check if any button has been Released
            if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released) || (previousMouseState.RightButton == ButtonState.Pressed && currentMouseState.RightButton == ButtonState.Released) || (previousMouseState.MiddleButton == ButtonState.Pressed && currentMouseState.MiddleButton == ButtonState.Released))
            {
                bool left = false, right = false, middle = false;

                if (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    left = true;
                }
                if (previousMouseState.RightButton == ButtonState.Pressed && currentMouseState.RightButton == ButtonState.Released)
                {
                    right = true;
                }
                if (previousMouseState.MiddleButton == ButtonState.Pressed && currentMouseState.MiddleButton == ButtonState.Released)
                {
                    middle = true;
                }

                //Fire event
                MouseReleased(left, right, middle, currentMouseState.Position);
            }

            //Check if the mouse has moved
            if(currentMouseState.Position != previousMouseState.Position)
            {
                //Fire event
                MouseMoved(currentMouseState);
            }
        }

        /// <summary>
        /// Method that fires the mouse down event
        /// </summary>
        /// <param name="mouseState">The current state of the mouse</param>
        public void MouseClicked(MouseState mouseState)
        {
            if (onMouseDown != null)
                onMouseDown(this, new NanoMouseEventArgs { CurrentMouseState = mouseState });
        }

        /// <summary>
        /// Method that fires the mouse up event
        /// </summary>
        /// <param name="mouseState">The current state of the mouse</param>
        public void MouseReleased(bool left, bool right, bool middle, Point pos)
        {
            if (onMouseUp != null)
                onMouseUp(this, new NanoMouseReleasedArgs { Left = left, Middle = middle, Right = right, Position = pos});
        }

        /// <summary>
        /// Method that fires the mouse moved event
        /// </summary>
        /// <param name="mouseState">The current state of the mouse</param>
        public void MouseMoved(MouseState mouseState)
        {
            if (onMouseMoved != null)
                onMouseMoved(this, new NanoMouseEventArgs { CurrentMouseState = mouseState });
        }
    }
}
