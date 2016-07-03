using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events.Interfaces
{
    public interface IHandler
    {
        /// <summary>
        /// Method that updates events
        /// </summary>
        void Update();
    }
}
