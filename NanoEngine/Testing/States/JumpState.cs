using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.States
{
    class JumpState : IState<IAiComponent>
    {
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Enter(IAiComponent owner)
        {
            (owner.ControledAsset as PhysicsEntity).ApplyForce(new Vector2(
                0, -5    
            ));

            (owner.ControledAsset as PhysicsEntity).Gravity = new Vector2(0, 0.7f);
            IsSuccess = true;
        }

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Exit(IAiComponent owner)
        {

        }

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        public void Update(IAiComponent owner)
        {

        }
    }
}
