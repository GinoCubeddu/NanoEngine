using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Testing.Assets
{
    class HexMind : AiComponent, IKeyboardWanted
    {
        /// <summary>
        /// Method that updates the AI
        /// </summary>
        public override void Update()
        {
            
        }

        public override void Initialise()
        {

        }

        /// <summary>
        /// Reciver for the keyboard change event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Left))
                    controledEntity.SetPosition(new Vector2(-1, 0));
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Right))
                    controledEntity.SetPosition(new Vector2(1, 0));
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Up))
                    controledEntity.SetPosition(new Vector2(0, -1));
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Down))
                    controledEntity.SetPosition(new Vector2(0, 1));
            }
        }
    }
}
