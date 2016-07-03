using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events.Args
{
    public class NanoKeyboardEventArgs : EventArgs
    {
        public IList<Keys> MyKeys;
    }
}
