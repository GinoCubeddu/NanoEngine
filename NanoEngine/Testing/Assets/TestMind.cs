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
using NanoEngine.Testing.States;

namespace NanoEngine.Testing.Assets
{
    class TestMind : AiComponent, IKeyboardWanted
    {
        private string Direction;

        public IStateMachine<IAiComponent> _StateMachine;

        public int Timer;

        public TestMind()
        {
            Timer = 0;
            Direction = "right";
        }

        public override void Initialise()
        {
            _StateMachine = new StateMachine<IAiComponent>(this);
            _StateMachine.AddState(new IdleState<IAiComponent>("idleRight"), "idle");
            _StateMachine.AddState(new WalkingState<IAiComponent>("runRight", 1), "runRight");
            _StateMachine.AddState(new WalkingState<IAiComponent>("runLeft", -1), "runLeft");
            _StateMachine.AddState(new HorizontalState<IAiComponent>("runRight", -1), "runUp");
            _StateMachine.AddState(new HorizontalState<IAiComponent>("runLeft", 1), "runDown");
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

        public override void Update()
        {
            Console.WriteLine(controledEntity.Position);
            _StateMachine.Update();
            Timer++;
        }

        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            _StateMachine.HandleKeyboardInput(args);
        }
    }
}