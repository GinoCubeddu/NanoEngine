using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.Collision;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Events.Args
{
    public class NanoCollisionEventArgs : EventArgs
    {
        // The asset that the object has collided with
        public IAsset CollidedWith { get; set; }

        // The side of the collision
        public CollisionSide CollisionSide { get; set; }

        // The amount of overlap that the objects have
        public Vector2 CollisionOverlap { get; set; }
    }
}
