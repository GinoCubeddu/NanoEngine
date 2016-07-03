using Microsoft.Xna.Framework;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    class TestAI : AIComponent, IMouseWanted
    {
        public TestAI()
        {

        }

        public override void Update()
        {
            controledEntity.Position = new Vector2(controledEntity.Position.X + 1, controledEntity.Position.Y);
        }

        /// <summary>
        /// Event Reciver for the mouse down event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseDown(object sender, NanoMouseEventArgs e)
        {
            Console.WriteLine("Mouse Pressed Down");
        }

        /// <summary>
        /// Event Reciver for mouse up event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseUp(object sender, NanoMouseEventArgs e)
        {
            Console.WriteLine("Uped");
        }

        /// <summary>
        /// Event Reciver for mouse moved event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseMoved(object sender, NanoMouseEventArgs e)
        {
            Console.WriteLine("moved");
        }
    }
}
