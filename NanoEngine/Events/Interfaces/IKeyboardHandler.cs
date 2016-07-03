using NanoEngine.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events.Interfaces
{
    public interface IKeyboardHandler : IHandler
    {
        /// <summary>
        /// Public getter and setter to control the OnKeyPressed event
        /// </summary>
        EventHandler<NanoKeyboardEventArgs> GetOnKeyPressed { get; set; }
        /// <summary>
        /// Public getter and setter to control the OnKeyDown event
        /// </summary>
        EventHandler<NanoKeyboardEventArgs> GetOnKeyDown { get; set; }
        /// <summary>
        /// Public getter and setter to control the OnKeyReleased
        /// </summary>
        EventHandler<NanoKeyboardEventArgs> GetOnKeyReleased { get; set; }
    }
}
