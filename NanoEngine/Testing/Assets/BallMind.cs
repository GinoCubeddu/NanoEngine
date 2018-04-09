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
    class BallMind : AiComponent, ICollisionResponder, IKeyboardWanted
    {
    

        public IStateMachine<IAiComponent> _StateMachine;

        private bool movingLeft = false;
        private bool movingRight = false;
        private bool movingUp = false;
        private bool movingDown = false;

        PhysicsManager physicsManager;


        public BallMind()
        {
            physicsManager = new PhysicsManager();
        }

        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public override void Update(IUpdateManager updateManager)
        {
            if (((PhysicsEntity)ControledAsset).Velocity.X > 10)
            {
                ((PhysicsEntity)ControledAsset).Velocity = new Vector2(0.6f, ((PhysicsEntity)ControledAsset).Velocity.Y);
            }
            if (((PhysicsEntity)ControledAsset).Velocity.Y > 10)
            {
                ((PhysicsEntity)ControledAsset).Velocity = new Vector2(((PhysicsEntity)ControledAsset).Velocity.X, 0.6f);
            } 

            if (movingUp)
                ((PhysicsEntity)ControledAsset).ApplyForce(new Vector2(0, -0.6f));

            if (movingDown)
                ((PhysicsEntity)ControledAsset).ApplyForce(new Vector2(0, 0.6f));


            if (movingLeft)
                ((PhysicsEntity)ControledAsset).ApplyForce(new Vector2(-0.6f, 0));

            if (movingRight)
                ((PhysicsEntity)ControledAsset).ApplyForce(new Vector2(0.6f, 0));

        }

        public override void Initialise()
        {

        }


        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.W))
                    movingUp = true;
                
            }
            if (args.TheKeys.ContainsKey(KeyStates.Released))
            {
                if (args.TheKeys[KeyStates.Released].Contains(Keys.W))
                    movingUp = false;
            }

            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.S))
                    movingDown = true;

            }
            if (args.TheKeys.ContainsKey(KeyStates.Released))
            {
                if (args.TheKeys[KeyStates.Released].Contains(Keys.S))
                    movingDown = false;
            }


            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.A))
                    movingLeft = true;

            }
            if (args.TheKeys.ContainsKey(KeyStates.Released))
            {
                if (args.TheKeys[KeyStates.Released].Contains(Keys.A))
                    movingLeft = false;
            }


            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D))
                    movingRight = true;

            }

            if (args.TheKeys.ContainsKey(KeyStates.Released))
            {
                if (args.TheKeys[KeyStates.Released].Contains(Keys.D))
                    movingRight = false;
            }
        }

        public void CollisionResponse(NanoCollisionEventArgs response)
        {

        }
    }
}
