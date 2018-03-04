﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Collision.CollidableTypes
{
    public interface ICollisionResponder
    {
        void CollisionResponse(CollisionResponse response);
    }
}
