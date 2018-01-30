using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine
{
    class TestAsset : Entity
    {
        public override void Initilise()
        {
            SetTexture(ContentManagerLoad.Manager.LoadResource<Texture2D>("player"));
        }
    }
}
