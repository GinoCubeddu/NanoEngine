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
    class TestMind : AiComponent, IKeyboardWanted
    {
        private string Direction;

        public IStateMachine _StateMachine;

        public int Timer;

        public TestMind()
        {
            Timer = 0;
            Direction = "right";
        }

        public override void Initialise()
        {
            _StateMachine = new StateMachine<AiComponent>(this);
            _StateMachine.AddState(new WalkingState());
        }

        public bool IsReadyToSwitch()
        {
            if (Timer > 360)
                return true;
            return false;
        }

        public override void Update()
        {
            _StateMachine.Update();
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
            _StateMachine.HandleKeyboardInput(args);
        }
    }
}