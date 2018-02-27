using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace NanoEngine.Testing.Assets
{
    class TestAsset : Entity
    {
        public override void Initilise()
        {
            SetTexture(ContentManagerLoad.Manager.LoadResource<Texture2D>("player"));

            AssetAnimation = new Animation(this);
            AssetAnimation.AddState("idleRight", 27, 64, 0, 8);
            AssetAnimation.AddState("idleLeft", 27, 64, 2, 8);
            AssetAnimation.AddState("runRight", 35, 64, 1, 6);
            AssetAnimation.AddState("runLeft", 35, 64, 3, 6);
        }
    }
}
