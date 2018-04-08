using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Collision;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Events.Args;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.StateMachine;
using NanoEngine.StateManagement.States;
using NanoEngine.Testing.States;
using NanoEngine.Physics;
using NanoEngine.Testing.Tiles;

namespace NanoEngine.Testing.Assets
{
    class TestMind : AiComponent, IKeyboardWanted, ICollisionResponder, IAssetmanagerNeeded, IMouseWanted
    {
        private string Direction;

        public IStateMachine<IAiComponent> _StateMachine;

        public int Timer;


        public TestMind()
        {
 
            Timer = 0;
            Direction = "right";
        }

        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public override void Update(IUpdateManager updateManager)
        {
            if (((PhysicsEntity) ControledAsset).Velocity.X > 3)
            {
                ((PhysicsEntity) ControledAsset).Velocity = new Vector2(3, ((PhysicsEntity)ControledAsset).Velocity.Y);
            }
            if (((PhysicsEntity)ControledAsset).Velocity.Y > 3)
            {
                ((PhysicsEntity)ControledAsset).Velocity = new Vector2(((PhysicsEntity)ControledAsset).Velocity.X, 3);
            }
            //((PhysicsEntity)controledEntity).Gravity = Vector2.Zero;
            _StateMachine.Update();
            Timer++;
        }

        public override void Initialise()
        {
            _StateMachine = new StateMachine<IAiComponent>(this);
            _StateMachine.AddState(new IdleState<IAiComponent>("idleRight"), "idle");
            _StateMachine.AddState(new WalkingState<IAiComponent>("runRight", 1), "runRight");
            _StateMachine.AddState(new WalkingState<IAiComponent>("runLeft", -1), "runLeft");
            _StateMachine.AddState(new HorizontalState<IAiComponent>("runRight", -1), "runUp");
            _StateMachine.AddState(new HorizontalState<IAiComponent>("runLeft", 1), "runDown");
            _StateMachine.AddState(new JumpState(), "jump");

            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.Space }, "idle", "jump"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.Space }, "runRight", "jump"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.Space }, "runLeft", "jump"
            );
            _StateMachine.AddSuccessTransition(
                "jump", "idle"
            );


            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.D }, "idle", "runRight"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.A }, "idle", "runLeft"
            );

            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() {Keys.D}, "runLeft", "runRight"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Released, new List<Keys>() { Keys.A }, "runLeft", "idle"
            );

            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.W }, "idle", "runUp"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.S }, "idle", "runDown"
            );


            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.A }, "runRight", "runLeft"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Released, new List<Keys>() { Keys.D }, "runRight", "idle"
            );

            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.W }, "runLeft", "runUp"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.W }, "runRight", "runUp"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.W }, "runDown", "runUp"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Released, new List<Keys>() { Keys.W }, "runUp", "idle"
            );

            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.S }, "runLeft", "runDown"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.S }, "runRight", "runDown"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Pressed, new List<Keys>() { Keys.S }, "runUp", "runDown"
            );
            _StateMachine.AddKeyboardTransition(
                KeyStates.Released, new List<Keys>() { Keys.S }, "runDown", "idle"
            );
        }

        public bool IsReadyToSwitch()
        {
            if (Timer > 360)
                return true;
            return false;
        }

        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            _StateMachine.HandleKeyboardInput(args, null);
            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D1))
                    controledEntity.Rotate(controledEntity.Position, -0.02f);
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D2))
                    controledEntity.Rotate(controledEntity.Position, 0.02f);
            }
            

        }

        public void CollisionResponse(NanoCollisionEventArgs response)
        {
            Console.WriteLine("PLAYER: " + response.CollisionSide);
            controledEntity.SetPosition(controledEntity.Position - response.CollisionOverlap);

            if (controledEntity.Points != null)
                foreach (IList<Vector2> points in controledEntity.Points.Values)
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i] -= response.CollisionOverlap;
                    }
                }
            if (response.CollidedWith.UniqueName.ToLower().Contains("coin"))
                response.CollidedWith.Despawn = true;
        }

        public IAssetManager AssetManager { get; set; }

        /// <summary>
        /// Event Reciver for the mouse down event
        /// </summary>
        /// <param name="sender">The object that sends the event</param>
        /// <param name="e">The arguments that are sent</param>
        public void OnMouseChanged(object sender, NanoMouseEventArgs e)
        {
            Console.WriteLine(e.CurrentMouseState.Position);
        }
    }
}