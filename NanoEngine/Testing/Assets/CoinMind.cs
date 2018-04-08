using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets.Control;

namespace NanoEngine.Testing.Assets
{
    class CoinMind : AiComponent
    {
        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public override void Update(IUpdateManager updateManager)
        {
            controledEntity.SetPosition(new Vector2(ControledAsset.Position.X + 1, controledEntity.Position.Y));
        }

        public override void Initialise()
        {
        }
    }
}
