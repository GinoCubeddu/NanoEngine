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
               
                //1. Compute the collision normal (‘cnormal’) - can reasonably estimate the collision normal as being the unit vector along a line drawn between the two circle origins
                Vector2 cn = Vector2.Normalize(eventArgs.Item1.CollisionOverlap * 2);

            
                //2. move the two balls apart a distance of ‘overlap’ along the collision normal:  
                //ball1.position += 0.5 * overlap * cnormal;  ball2.position -= 0.5 * overlap * cnormal;
                //((PhysicsEntity)asset1).Position += 0.5f * eventArgs.Item1.CollisionOverlap * cn;
                //((PhysicsEntity)asset2).Position -= 0.5f * eventArgs.Item2.CollisionOverlap * cn;

                asset1.SetPosition(asset1.Position - eventArgs.Item1.CollisionOverlap);
                asset2.SetPosition(asset2.Position - eventArgs.Item2.CollisionOverlap);


                //3. Compute the closing velocity from -- the dot product of the collision normal with the difference of the entity velocities
                //note:c# Dot - Calculates the dot product of two vectors. If the two vectors are unit vectors, 
                //the dot product returns a floating point value between -1 and 1 that can be used to determine some properties of the angle between two vectors

                float cvA = Vector2.Dot(cn, ((PhysicsEntity)asset1).Velocity);
                float cvB = Vector2.Dot(-cn, ((PhysicsEntity)asset2).Velocity);

                //4. multiply the result with the collision normal to get the true closing velocity -The result of this is a scalar (ie speed, not velocity) 
                //get transferring velocity
                Vector2 velocityA = (cn * cvB) - (cn * cvA) *  ((PhysicsEntity)asset1).InverseMass;
                Vector2 velocityB = (cn * cvA) - (-cn * cvB) *  ((PhysicsEntity)asset2).InverseMass;
                // We just essentially moved the component from element A and B to get the remaining closing velocity value we now need to add that with the elements velocity i.e.
                //5. Apply the resulting velocity vector to the two entities using the ‘ApplyImpulse’ methods.  the entities must be reflected in opposite directions

               ((PhysicsEntity)asset1).ApplyImpluse(velocityA * cn);
               ((PhysicsEntity)asset2).ApplyImpluse(velocityB * cn);

            }
            else if (asset1 is IBounce)
            {
                //1. use mtv, (Interpenetration) to ensure th ball is just touching the wall
                asset1.SetPosition(asset1.Position - eventArgs.Item1.CollisionOverlap);
                //2. computing the cn, i.e direction particle is traveling at
                Vector2 cn = Vector2.Normalize(eventArgs.Item1.CollisionOverlap * 2);
                //3. compute the true cv, i.e the veclocity in the direction of the cn
                float cvA = Vector2.Dot(cn, ((PhysicsEntity)asset1).Velocity);
                //4. rebound - apply twice the closing velocity to the collision cn
                Vector2 velocityA = -2 * (cvA * cn);

                //5.apply velocity to impulse on asset 
                ((PhysicsEntity)asset1).ApplyImpluse(velocityA);
            } else if (asset2 is IBounce)
            {
                //1. use mtv, (Interpenetration) to ensure th ball is just touching the wall
                asset2.SetPosition(asset2.Position - eventArgs.Item2.CollisionOverlap);
                //2. computing the cn, i.e direction particle is traveling at
                Vector2 cn = Vector2.Normalize(eventArgs.Item2.CollisionOverlap * 2);
                //3. compute the true cv, i.e the veclocity in the direction of the cn
                float cvB = Vector2.Dot(cn, ((PhysicsEntity)asset2).Velocity);
                //4. rebound - apply twice the closing velocity to the collision cn
                Vector2 velocityB = -2 * (cvB * cn );
                //5. apply velocity to impulse on asset 1
                ((PhysicsEntity)asset2).ApplyImpluse(velocityB);
            }


        }


    }
}
