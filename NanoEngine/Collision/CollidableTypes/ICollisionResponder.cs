using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Events.Args;

namespace NanoEngine.Collision.CollidableTypes
{
    public interface ICollisionResponder
    {
        /// <summary>
        /// Recives an event from the collision manager when a collision happens
        /// with controled entity
        /// </summary>
        /// <param name="response">Collision args containing nessecery data</param>
        void CollisionResponse(NanoCollisionEventArgs response);
    }
}
