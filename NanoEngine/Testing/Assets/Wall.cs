using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Assets
{
    class Wall : Entity, IAABBColidable
    {
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager).LoadResource<Texture2D>("wall-vert"));
        }
    }
}
