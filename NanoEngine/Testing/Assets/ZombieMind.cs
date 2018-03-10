using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.Assets.Control;
using NanoEngine.StateManagement.StateMachine;
using NanoEngine.Testing.States;

namespace NanoEngine.Testing.Assets
{
    public class ZombieMind : AiComponent, IAssetmanagerNeeded
    {
        private IStateMachine<IAiComponent> _stateMachine;

        public IAssetManager AssetManager { get; set; }

        /// <summary>
        /// Method that will update the AI
        /// </summary>
        /// <param name="updateManager">an instance of the update manager</param>
        public override void Update(IUpdateManager updateManager)
        {
            _stateMachine.Update();
        }

        public override void Initialise()
        {
            // Init a new state machine
            _stateMachine = new StateMachine<IAiComponent>(this);

            // Add a patroling state
            _stateMachine.AddState(
                new PatrolState<IAiComponent>(
                    "walkRight",
                    "walkLeft",
                    new List<Vector2>()
                    {
                        new Vector2(controledEntity.Position.X + 250, controledEntity.Position.Y),
                        controledEntity.Position
                    }),
                "patrol"
            );

            // Add a "cheer" state
            _stateMachine.AddState(new CheerState<IAiComponent>("cheer"), "cheer");
            
            // Add a chase state
            _stateMachine.AddState(
                new ChaseState<IAiComponent>(
                    "walkRight",
                    "walkLeft",
                    AssetManager.RetriveAsset("player")
                ),
                "chase"
            );

            // Set up all the transitions for the states
            _stateMachine.AddSuccessTransition("patrol", "cheer");
            _stateMachine.AddSuccessTransition("cheer", "patrol");
            _stateMachine.AddMethodCheckTransition(CloseToPlayer, "patrol", "chase");
            _stateMachine.AddMethodCheckTransition(CloseToPlayer, "chase", "patrol", false);
        }

        /// <summary>
        /// Checks to see if the Ai is close to the player
        /// </summary>
        /// <returns></returns>
        private bool CloseToPlayer()
        {
            // Grab the players position
            Vector2 playersPosition = AssetManager.RetriveAsset("player").Position;

            // Check if the player is within 250 px of the enemey
            if (Vector2.Distance(controledEntity.Position, playersPosition) < 250)
                return true;
            return false;
        }
    }
}
