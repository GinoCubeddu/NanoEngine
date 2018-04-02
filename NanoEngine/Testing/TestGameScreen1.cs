using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.General;

namespace NanoEngine.Testing
{
    class TestGameScreen1 : GameScreen
    {
        /// <summary>
        /// Abstract method to force sub classes to implement it. It is used to load content to the screen 
        /// </summary>
        public override void LoadContent()
        {
            _assetManager.CreateAsset<RedRunner>("redRunner", 100, 100);
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
