using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine
{
    public interface IState <T>
    {
        // Boolean telling us if the state was a success
        bool IsSuccess { get; }

        /// <summary>
        /// Method that gets called at the begining of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        void Enter(T owner, IDictionary<string, object> stateArguments);

        /// <summary>
        /// Method that gets called at the end of each state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        void Exit(T owner);

        /// <summary>
        /// Method that updates the state
        /// </summary>
        /// <typeparam name="T">The type of AI that the state uses</typeparam>
        /// <param name="owner">The AI that owns the state</param>
        /// <param name="updateManager">Instance of the update manager</param>
        void Update(IUpdateManager updateManager, T owner);
    }
}
