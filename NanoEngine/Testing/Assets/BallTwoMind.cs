using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Collision;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.StateMachine;
using NanoEngine.StateManagement.States;
using NanoEngine.Testing.States;
using NanoEngine.Physics;
namespace NanoEngine.Testing.Assets
{
    class BallTwoMind : AiComponent, ICollisionResponder
    {
        public void CollisionResponse(NanoCollisionEventArgs response)
        {

        }

        public override void Initialise()
        {

        }

        public override void Update(IUpdateManager updateManager)
        {
            if (((PhysicsEntity)ControledAsset).Velocity.X > 10)
            {
                ((PhysicsEntity)ControledAsset).Velocity = new Vector2(5, ((PhysicsEntity)ControledAsset).Velocity.Y);
            }
            if (((PhysicsEntity)ControledAsset).Velocity.Y > 10)
            {
                ((PhysicsEntity)ControledAsset).Velocity = new Vector2(((PhysicsEntity)ControledAsset).Velocity.X, 5);
            }
        }
    }
}
