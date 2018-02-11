using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.StateManagement.StateMachine;

namespace NanoEngine.Testing
{
    public class ZombieMind : AiComponent, IAssetmanagerNeeded
    {
        private IStateMachine _stateMachine;

        public IAssetManager AssetManager { get; set; }

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
            _stateMachine.AddState(new ChaseState(AssetManager.RetriveAsset("player")), "chase");
            _stateMachine.AddSuccessTransition("patrol", "cheer");
            _stateMachine.AddSuccessTransition("cheer", "patrol");
            _stateMachine.AddMethodCheckTransition(CloseToPlayer, "patrol", "chase");
        }

        private bool CloseToPlayer()
        {
            Vector2 playersPosition = AssetManager.RetriveAsset("player").Position;
            if (Vector2.Distance(controledEntity.Position, playersPosition) < 100)
                return true;
            return false;
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
