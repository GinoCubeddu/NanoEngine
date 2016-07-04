using NanoEngine.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Control
{
    public interface IMouseWanted
    {
        /// <summary>
        /// Event Reciver for the mouse down event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        void OnMouseDown(object sender, NanoMouseEventArgs e);

        /// <summary>
        /// Event Reciver for mouse up event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        void OnMouseUp(object sender, NanoMouseReleasedArgs e);

        /// <summary>
        /// Event Reciver for mouse moved event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        void OnMouseMoved(object sender, NanoMouseEventArgs e);
    }
}
