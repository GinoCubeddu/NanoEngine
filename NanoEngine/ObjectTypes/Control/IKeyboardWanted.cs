using NanoEngine.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Control
{
    public interface IKeyboardWanted
    {
        /// <summary>
        /// Reciver for the key pressed event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        void OnKeyPressed(object sender, NanoKeyboardEventArgs args);

        /// <summary>
        /// Reciver for the key Released event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        void OnKeyReleased(object sender, NanoKeyboardEventArgs args);

        /// <summary>
        /// Reciver for the key Down event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        void OnKeyDown(object sender, NanoKeyboardEventArgs args);
    }
}
