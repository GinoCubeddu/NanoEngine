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

namespace NanoEngine.Testing.Tiles
{
    class TileMind : AiComponent, ICollisionResponder
    {
        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public override void Update(IUpdateManager updateManager)
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


            if (response.CollisionSide == CollisionSide.TOP && response.CollidedWith is PhysicsEntity)
            {
                (response.CollidedWith as PhysicsEntity).Gravity = Vector2.Zero;
                (response.CollidedWith as PhysicsEntity).Velocity = new Vector2((response.CollidedWith as PhysicsEntity).Velocity.X, 0);
            }
        }
    }
}
