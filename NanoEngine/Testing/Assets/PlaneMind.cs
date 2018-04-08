using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Testing.Assets
{
    class PlaneMind : AiComponent, IKeyboardWanted
    {
        public override void Update(IUpdateManager updateManager)
        {

        }

        public override void Initialise()
        {

        }

        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D1))
                    controledEntity.Rotate(new Vector2(controledEntity.Bounds.Width / 2f, ControledAsset.Bounds.Height / 2f), -0.02f);
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D2))
                    controledEntity.Rotate(new Vector2(controledEntity.Bounds.Width / 2f, ControledAsset.Bounds.Height / 2f), 0.02f);
            }
        }
    }
}
