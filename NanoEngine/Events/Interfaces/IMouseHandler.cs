using NanoEngine.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events.Interfaces
{
    public interface IMouseHandler : IHandler
    {
        /// <summary>
        /// Getter/Setter to return the event when the mouse is being clicked
        /// </summary>
        EventHandler<NanoMouseEventArgs> GetOnMouseDown { get; set; }

        /// <summary>
        /// Getter/Setter to return the event when the mouse is no longer being clicked
        /// </summary>
        EventHandler<NanoMouseEventArgs> GetOnMouseUp { get; set; }

        /// <summary>
        /// Getter/Setter to return the event when the mouse has been moved
        /// </summary>
        EventHandler<NanoMouseEventArgs> GetOnMouseMoved { get; set; }
    }
}
