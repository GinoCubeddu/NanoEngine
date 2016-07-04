using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events.Args
{
    public class NanoMouseReleasedArgs : EventArgs
    {
        //Bools to tell which buttons were released
        public bool Left, Right, Middle;

        //Point containing the current position of the mouse
        public Point Position;
    }
}
