using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.Testing.Physics;


namespace NanoEngine.Testing.Assets
{
    class TestAsset : PhysicsEntity, ISATColidable, IReflect, IBounce
    {
        public override void Initilise()
        {
            IsMovable = true;
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Texture2D>("player"));
            AssetAnimation = new Animation.Animation(this);
            AssetAnimation.AddState("idleRight", 27, 64, 8, 12, 0, 0);
            AssetAnimation.AddState("idleLeft", 27, 64, 8, 12, 128, 0);
            AssetAnimation.AddState("runRight", 35, 64, 6, 12, 64, 0);
            AssetAnimation.AddState("runLeft", 35, 64, 6, 12, 192, 0);
        }

        public int CollidableId => 1;
    }
}
