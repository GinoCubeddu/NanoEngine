using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.General;
using NanoEngine.Testing.Assets;

namespace NanoEngine.Testing
{
    class TestScreen2 : GameScreen
    {
        /// <summary>
        /// Abstract method to force sub classes to implement it. It is used to load content to the screen 
        /// </summary>
        public override void LoadContent()
        {
            //_assetManager.CreateAsset<plane, PlaneMind>(400, 400);
            _assetManager.CreateAsset<Hex, TestMind>("player", 400, 300);
            _assetManager.CreateAsset<plane>(400, 400);

        }

        /// <summary>
        /// Abstract method to force sub classes to implement it. It is used to update the screen 
        /// </summary>
        /// <param name="updateManager">An instance of the update manager</param>
        protected override void Update(IUpdateManager updateManager)
        {
        }

        /// <summary>
        /// Abstarct method to force sub classes to implement it
        /// </summary>
        /// <param name="renderManager">An instance of the render manager which will be used to draw</param>
        protected override void Draw(IRenderManager renderManager)
        {
        }
    }
}
