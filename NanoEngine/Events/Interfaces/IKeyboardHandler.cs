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
        /// Sends an event when the keyboard state chanages
        /// </summary>
        /// <param name="pKeys">A dict conatining the keys and what state they are in</param>
        EventHandler<NanoKeyboardEventArgs> GetOnKeyboardChanged { get; set; }
    }
}
