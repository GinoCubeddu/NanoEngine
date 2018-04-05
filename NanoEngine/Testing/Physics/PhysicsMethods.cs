using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.Physics;
using Microsoft.Xna.Framework;
using NanoEngine.Events.Args;

namespace NanoEngine.Testing.Physics
{
    static class PhysicsMethods
    {
        public static void Bounce(IAsset asset1, IAsset asset2, Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs> eventArgs)
        {
            if (asset1 is IBounce && asset2 is IBounce)
            {
                //bounce the balls off each other

                //A vector can be seen as pointing to a specific coordinate. It can also be seen as having a direction and a magnitude (or length).
                //A normalized vector is one that has a magnitude
                //(or length) of exactly 1.Normalizing a vector produces a vector pointing in exactly the same direction, with a length of exactly 1.

                //1. Compute the interpenetration (‘overlap’) -the same ‘line’ that you used to determine the distance between the circle origins for the collision test
              //  Vector2 overlap = asset1.Position - asset2.Position;
                Vector2 overlap = ((PhysicsEntity)asset1).Position - ((PhysicsEntity)asset2).Position;
     

                //dist = find mag of collision normal cn.mag() (in java)
                // double magnitude = Math.Sqrt(overlap.X * overlap.X + overlap.Y * overlap.Y);
                //2. Compute the collision normal (‘cnormal’) - can reasonably estimate the collision normal as being the unit vector along a line drawn between the two circle origins
                Vector2 cn = Vector2.Normalize(overlap);
               // Console.WriteLine("cn "+ cn);

                //3. move the two balls apart a distance of ‘overlap’ along the collision normal:  
                //ball1.position += 0.5 * overlap * cnormal;  ball2.position -= 0.5 * overlap * cnormal;
                ((PhysicsEntity)asset1).Position += 0.5f * eventArgs.Item1.CollisionOverlap * cn;
                ((PhysicsEntity)asset2).Position += 0.5f * eventArgs.Item2.CollisionOverlap * cn;

                //4. Compute the closing velocity from -- the dot product of the collision normal with the difference of the entity velocities
                //note:c# Dot - Calculates the dot product of two vectors. If the two vectors are unit vectors, 
                //the dot product returns a floating point value between -1 and 1 that can be used to determine some properties of the angle between two vectors

                float cvA = Vector2.Dot(cn, ((PhysicsEntity)asset1).Velocity);
                float cvB = Vector2.Dot(cn, ((PhysicsEntity)asset2).Velocity);

                //5. multiply the result with the collision normal to get the true closing velocity -The result of this is a scalar (ie speed, not velocity) 
                //get transferring velocity
                Vector2 velocityA = (cn * cvB) - (cn * cvA);
                Vector2 velocityB = (cn * cvA) - (cn * cvB);
                // We just essentially moved the component from element A and B to get the remaining closing velocity value we now need to add that with the elements velocity i.e.
                //6. Apply the resulting velocity vector to the two entities using the ‘ApplyImpulse’ methods.  the entities must be reflected in opposite directions

                ((PhysicsEntity)asset1).ApplyImpluse(velocityA * cn);
                ((PhysicsEntity)asset2).ApplyImpluse(velocityB * cn);

            }
            else if (asset1 is IBounce)
            {
                //reflect off plane
                ((PhysicsEntity)asset1).ApplyImpluse(((PhysicsEntity)asset1).Velocity * -2);
            } else if (asset2 is IBounce)
            {
                //reflect off plane
               ((PhysicsEntity)asset2).ApplyImpluse(((PhysicsEntity)asset1).Velocity * -2);
            }
               
            //if (asset2 is IBounce)
                //Console.WriteLine("Asset 2 is IBOUNCE = ");
        }

        public static void Reflect(IAsset asset1, IAsset asset2)
        {
            /*Vector2 controlledEntityVelocity = ((PhysicsEntity)ControledAsset).Velocity;

            Vector2 overlap = response.CollisionOverlap;

            Vector2 cn = Vector2.Normalize(overlap);

            ((PhysicsEntity)ControledAsset).Position += 0.5f * overlap * cn;


            float cvA = Vector2.Dot(cn, controlledEntityVelocity);

            Vector2 velocityA =  (cn * cvA);

            ((PhysicsEntity)ControledAsset).ApplyImpluse(velocityA); */

          // ((PhysicsEntity)asset1).ApplyForce(((PhysicsEntity) asset1).Velocity *-2);
          // ((PhysicsEntity)asset2).ApplyForce(((PhysicsEntity)asset1).Velocity * -2);

           /* if (asset1 is IReflect)
                Console.WriteLine("Asset 1 is IREFLECT = " + asset1.Position);
            if (asset2 is IReflect)
                Console.WriteLine("Asset 2 is IREFLECT = " + asset2.Position); */
        }
    }
}
