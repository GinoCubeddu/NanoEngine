using NanoEngine.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Events;

namespace NanoEngine.ObjectTypes.Control
{
    public interface IMouseWanted : INanoEventSubscribe
    {
        /// <summary>
        /// Event Reciver for the mouse down event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        void OnMouseChanged(object sender, NanoMouseEventArgs e);
    }
}
