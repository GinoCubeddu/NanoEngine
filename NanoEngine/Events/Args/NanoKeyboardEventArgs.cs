using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Events.Args
{
    public class NanoKeyboardEventArgs : EventArgs
    {
        public IList<Keys> MyKeys;
        public IDictionary<KeyStates, IList<Keys>> TheKeys;
    }
}
