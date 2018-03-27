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
    class TestAsset : PhysicsEntity, IAABBColidable, IReflect, IBounce
    {
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Texture2D>("player"));
            Gravity = Vector2.Zero;
            AssetAnimation = new Animation(this);
            AssetAnimation.AddState("idleRight", 27, 64, 0, 8);
            AssetAnimation.AddState("idleLeft", 27, 64, 2, 8);
            AssetAnimation.AddState("runRight", 35, 64, 1, 6);
            AssetAnimation.AddState("runLeft", 35, 64, 3, 6);
        }
    }
}
