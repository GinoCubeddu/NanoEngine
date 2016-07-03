using Microsoft.Xna.Framework;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    class TestScreen : GameScreen
    {
        public override void LoadContent()
        {
            EntityManager.Manager.AddEntity<TestEntity>(50, 50, "hi");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw()
        {

        }

        public override void Update()
        {

        }
    }
}
