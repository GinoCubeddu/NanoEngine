using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Assets
{
    public class ZombieAsset : Entity, IAABBColidable
    {
        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Texture2D>("zombie")); AssetAnimation = new Animation(this);
            AssetAnimation.AddState("walkLeft", 50, 64, 0, 2, 4);
            AssetAnimation.AddState("walkRight", 50, 64, 1, 2, 4);
            AssetAnimation.AddState("cheer", 50, 64, 2, 2, 4);
        }
    }
}
