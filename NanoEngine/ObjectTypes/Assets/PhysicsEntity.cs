using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NanoEngine.ObjectTypes.Assets
{
    public abstract class PhysicsEntity : Entity
    {
        // The acceleration is the speed that the velocitey increases
        public Vector2 Acceleration { get; set; } = new Vector2(0, 0);

        // The velocity is the speed that the asset moves
        public Vector2 Velocity { get; set; } = new Vector2(0, 0);

        // The InverseMass is half the actual mass of the object
        public float InverseMass { get; set; } = 3f;

        // The damping is how much resistance the object has
        public float Damping { get; set; } = 0.95f;

        // The gravity is how fast a force pushes down on an object
        public Vector2 Gravity { get; set; } = new Vector2(0, 0.2f);

        /// <summary>
        /// Applys a force to the Acceleration of the asset
        /// </summary>
        /// <param name="force">The level of force to be applied</param>
        public virtual void ApplyForce(Vector2 force)
        {
            // Add the force multiplied by the inverseMass to the Acceleration
            Acceleration += force * InverseMass;
        }
    }
}
