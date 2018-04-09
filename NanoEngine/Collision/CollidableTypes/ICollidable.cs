using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Collision.CollidableTypes
{
    public interface ICollidable
    {
        // An ID to identify the type of collidable
        int CollidableId { get; }
    }
}
