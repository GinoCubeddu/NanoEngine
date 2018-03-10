using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Control;

namespace NanoEngine.StateManagement.Transitions
{
    class CollisionTransition
    {
        // Holds the type of which state to transition to
        public string StateTo { get; }

        // Holds the type that the asset must collide with to triger the transition
        private Type _collidableType;

        /// <summary>
        /// Creates the collision transiition
        /// </summary>
        /// <param name="stateTo">The state it should transition to</param>
        /// <param name="collidedWith">The type the to expect</param>
        public CollisionTransition(string stateTo, Type collidableType)
        {
            StateTo = stateTo;
            _collidableType = collidableType;
        }

        /// <summary>
        /// Method that checks if the transition is valid
        /// </summary>
        /// <param name="eventArgs">event args holding the nessecery incofmation</param>
        /// <returns>Boolean telling us if it is a valid transition</returns>
        public bool CheckTransition(NanoCollisionEventArgs eventArgs)
        {
            if (_collidableType.IsInstanceOfType(eventArgs.CollidedWith))
                return true;

            return false;
        }
    }
}
