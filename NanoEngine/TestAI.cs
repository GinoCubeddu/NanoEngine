using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    class TestAI : AIComponent, IKeyboardWanted
    {
        Vector2 Pos;

        public TestAI()
        {

        }

        public override void Update()
        {
            Pos = controledEntity.Position;
            controledEntity.Position = Pos;
        }

        /// <summary>
        /// Reciver for the key pressed event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyPressed(object sender, NanoKeyboardEventArgs args)
        {

        }

        /// <summary>
        /// Reciver for the key Released event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyReleased(object sender, NanoKeyboardEventArgs args)
        {

        }

        /// <summary>
        /// Reciver for the key Down event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyDown(object sender, NanoKeyboardEventArgs args)
        {

        }
    }
}
