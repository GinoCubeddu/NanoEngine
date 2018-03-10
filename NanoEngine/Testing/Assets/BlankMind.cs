using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets.Control;

namespace NanoEngine.Testing.Assets
{
    class BlankMind : AiComponent
    {
        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public override void Update(IUpdateManager updateManager)
        {
            throw new NotImplementedException();
        }

        public override void Initialise()
        {

        }
    }
}
