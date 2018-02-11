using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.StateManagement.StateMachine;

namespace NanoEngine.Testing
{
    public class ZombieMind : AiComponent
    {
        private IStateMachine _stateMachine;

        public override void Initialise()
        {
            _stateMachine = new StateMachine<IAiComponent>(this);
            _stateMachine.AddState(
                new PatrolState(
                    new List<string>() {"walkRight", "walkLeft"},
                    new List<Vector2>()
                    {
                        new Vector2(controledEntity.Position.X + 250, controledEntity.Position.Y),
                        controledEntity.Position
                    }),
                "patrol"
            );

            _stateMachine.AddState(new CheerState("cheer"), "cheer");
            _stateMachine.AddSuccessTransition("patrol", "cheer");
            _stateMachine.AddSuccessTransition("cheer", "patrol");
        }

        private bool CloseToPlayer()
        {

        }

        /// <summary>
        /// Method that updates the AI
        /// </summary>
        public override void Update()
        {
            _stateMachine.Update();
            
        }
    }
}
