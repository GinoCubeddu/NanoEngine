using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.StateMachine;
using NanoEngine.StateManagement.States;

namespace NanoEngine.Testing
{
    class TestMind : AIComponent, IKeyboardWanted
    {
        private string Direction;

        public IStateMachine _StateMachine;

        public int Timer;

        public TestMind()
        {
            Timer = 0;
            _StateMachine = new StateMachine();
            _StateMachine.AddState(new State1());
            _StateMachine.AddState(new State2());
            _StateMachine.AddState(new State3());
            _StateMachine.AddKeyboardTransition<State1, State2>(
                KeyStates.Pressed, new List<Keys>() { Keys.NumPad1 }
            );
            _StateMachine.AddKeyboardTransition<State2, State3>(
                KeyStates.Pressed, new List<Keys>() { Keys.NumPad2 }    
            );
            _StateMachine.AddMethodCheckTransition<State3, State1>(IsReadyToSwitch);
            Direction = "right";
        }

        public bool IsReadyToSwitch()
        {
            if (Timer > 360)
                return true;
            return false;
        }

        public void OnKeyDown(object sender, NanoKeyboardEventArgs args)
        {

            if (args.MyKeys.Contains(Keys.Left) & Direction == "left")
            {
                controledEntity.AssetAnimation.ChangeAnimationState("runLeft");
                controledEntity.SetPosition(new Vector2(
                    controledEntity.Position.X - 3, controledEntity.Position.Y
                ));
            }
                
            else if (args.MyKeys.Contains(Keys.Right) & Direction == "right")
            {
                controledEntity.AssetAnimation.ChangeAnimationState("runRight");
                controledEntity.SetPosition(new Vector2(
                    controledEntity.Position.X + 3, controledEntity.Position.Y
                ));
            }                

            if (args.MyKeys.Contains(Keys.S))
                controledEntity.AssetAnimation.ChangeAnimationState("idleRight");
        }

        public void OnKeyPressed(object sender, NanoKeyboardEventArgs args)
        {
            _StateMachine.HandleKeyboardInput(this, args);
        }

        public void OnKeyReleased(object sender, NanoKeyboardEventArgs args)
        {
            for (int i = 0; i < args.MyKeys.Count; i++)
                Console.WriteLine(args.MyKeys[i]);
            if (args.MyKeys.Contains(Keys.Left) && Direction == "left")
                controledEntity.AssetAnimation.ChangeAnimationState("idleLeft");
            if (args.MyKeys.Contains(Keys.Right) && Direction == "right")
                controledEntity.AssetAnimation.ChangeAnimationState("idleRight");
        }

        public override void Update()
        {
            _StateMachine.Update<IAIComponent>(this);
            Timer++;
        }
    }
}