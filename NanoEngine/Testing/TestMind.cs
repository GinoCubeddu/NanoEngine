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

        public override void Update()
        {
            _StateMachine.Update<IAIComponent>(this);
            Timer++;
        }

        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            foreach (KeyStates key in args.TheKeys.Keys)
            {
                Console.WriteLine(key.ToString().ToUpper());
                foreach (Keys item in args.TheKeys[key])
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine(args.TheKeys.Values);
            _StateMachine.HandleKeyboardInput(this, args);
        }
    }
}