using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Events.Args;

namespace NanoEngine.Collision.CollidableTypes
{
    public interface ICollisionResponder
    {
        void CollisionResponse(NanoCollisionEventArgs response);
    }
}
