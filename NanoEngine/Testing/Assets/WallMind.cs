using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NanoEngine.Collision;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;

namespace NanoEngine.Testing.Assets
{
    class WallMind : AiComponent, ICollisionResponder
    {
        public override void Update(IUpdateManager updateManager)
        {
        }

        public override void Initialise()
        {
        }

        public void CollisionResponse(NanoCollisionEventArgs response)
        {
            if (!response.CollidedWith.UniqueName.ToLower().Contains("wall"))
            {
                //if (response.CollisionSide == CollisionSide.BOTTOM)
                //    response.CollidedWith.SetPosition(new Vector2(response.CollidedWith.Position.X, ControledAsset.Position.Y - response.CollidedWith.Bounds.Height));
                //if (response.CollisionSide == CollisionSide.TOP)
                //    response.CollidedWith.SetPosition(new Vector2(response.CollidedWith.Position.X, ControledAsset.Bounds.Bottom));
                //if (response.CollisionSide == CollisionSide.LEFT)
                //    response.CollidedWith.SetPosition(new Vector2(ControledAsset.Bounds.Right, response.CollidedWith.Position.Y));
                //if (response.CollisionSide == CollisionSide.RIGHT)
                //    response.CollidedWith.SetPosition(new Vector2(ControledAsset.Position.X - response.CollidedWith.Bounds.Width, response.CollidedWith.Position.Y));
            }
        }
    }
}
