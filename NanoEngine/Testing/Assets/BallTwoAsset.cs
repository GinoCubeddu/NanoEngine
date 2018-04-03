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

using System.Threading.Tasks;

namespace NanoEngine.Testing.Assets
{
    class BallTwolAsset : PhysicsEntity, IAABBColidable, IBounce
    {
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
               .LoadResource<Texture2D>("ball2"));
           // Gravity = new Vector2(0, 0.2f);
        }
    }
}
