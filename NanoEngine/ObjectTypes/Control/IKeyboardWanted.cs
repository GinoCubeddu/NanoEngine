using NanoEngine.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Events;

namespace NanoEngine.ObjectTypes.Control
{
    public interface IKeyboardWanted : INanoEventSubscribe
    {
        /// <summary>
        /// Reciver for the keyboard change event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        void OnKeyboardChange(object sender, NanoKeyboardEventArgs args);
    }
}
