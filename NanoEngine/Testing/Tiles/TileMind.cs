using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets.Control;

namespace NanoEngine.Testing.Tiles
{
    class TileMind : AiComponent, ICollisionResponder
    {
        public override void Update()
        {

        }

        public override void Initialise()
        {

        }

        public void CollisionResponse(NanoCollisionEventArgs response)
        {
            Console.WriteLine("TILE: " + response.CollisionSide);
            response.CollidedWith.SetPosition(new Vector2(
                response.CollidedWith.Position.X + response.CollisionOverlap.X,
                response.CollidedWith.Position.Y + response.CollisionOverlap.Y
            ));
        }
    }
}
