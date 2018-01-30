using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    class TestMind : AIComponent
    {
        public override void Update()
        {
            controledEntity.SetPosition(new Vector2(
                controledEntity.Position.X + 1,
                controledEntity.Position.Y
            ));
        }
    }
}
