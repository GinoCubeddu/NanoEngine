using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision.CollisionTypes
{
    public class SAT : ISAT
    {
        /// <summary>
        /// Checks to see if 2 two objects are colliding coliding
        /// </summary>
        /// <param name="asset1">The first asset to be checked against</param>
        /// <param name="asset2">The second asset to be checked against</param>
        /// <returns>A boolean value telling us if there has been a collision</returns>
        public Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs> CheckCollision(IAsset asset1, IAsset asset2)
        {
            // set the default values
            bool collided = false;
            Vector2 smallestAxis = Vector2.Zero;
            float smallestOverlap = 999;

            // Get the points from asset A
            IDictionary<string, IList<Vector2>> asset1Points = asset1.Points;

            // If those points are null get them from the bounds
            if (asset1Points == null)
            {
                asset1Points = new Dictionary<string, IList<Vector2>>();
                asset1Points["body"] = asset1.GetPointsFromBounds();
            }

            // Do the same for asset B
            IDictionary<string, IList<Vector2>> asset2Points = asset2.Points;
            
            if (asset2Points == null)
            {
                asset2Points = new Dictionary<string, IList<Vector2>>();
                asset2Points["body"] = asset2.GetPointsFromBounds();
            }

            // Check each "object" in asset A against each "object" in asset B
            // Loop through each "object" in asset1
            foreach (string asset1PointKey in asset1Points.Keys)
            {
                // Loop through each "object" in asset 2
                foreach (string asset2PointKey in asset2Points.Keys)
                {
                    // Check current "object" of asset 1 against current "object" of asset 2
                    // Check current "object" of asset 2 against current "object" of asset 1
                    // If both of them return true then there is a collision however if there is a gap
                    // on either of the checks then it is impossible for there to be a collision
                    if (
                        CheckOverLap(asset1Points[asset1PointKey], asset2Points[asset2PointKey], ref smallestAxis, ref smallestOverlap) &&
                        CheckOverLap(asset2Points[asset2PointKey], asset1Points[asset1PointKey], ref smallestAxis, ref smallestOverlap)
                    )
                    {
                        // If none returned false there is a collision between the two objects
                        // and we dont have to check any other objects
                        collided = true;
                        break;
                    }
                }

                // If there is a collision then break out of thej loop
                if (collided)
                    break;
            }

            if (collided)
            {
                // The MTV is calcutated by multiplying the smallest axis by the smallet overlap
                // we then multiply the mtv by 0.5 as each entity will need to move half each
                Vector2 mtv = smallestAxis * smallestOverlap * 0.5f;

                if (Vector2.Dot(mtv, (asset2.Position - asset1.Position)) < 0)
                    mtv = mtv * -1;

                // Return the collision args for this collision
                return new Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs>(
                    new NanoCollisionEventArgs()
                    {
                        CollidedWith = asset2,
                        CollisionOverlap = mtv,
                        CollisionSide = CollisionSide.UNKNOWN
                    },
                    new NanoCollisionEventArgs()
                    {
                        CollidedWith = asset1,
                        CollisionOverlap = -mtv,
                        CollisionSide = CollisionSide.UNKNOWN
                    }
                );
            }
            // return null if no collision happened
            return null;
        }

        /// <summary>
        /// Checks to see if 2 lists of points overlap
        /// </summary>
        /// <param name="asset1Points">The first list of points</param>
        /// <param name="asset2Points">The second list of points</param>
        /// <returns>Boolean telling us if they have overlapped</returns>
        private bool CheckOverLap(IList<Vector2> asset1Points, IList<Vector2> asset2Points, ref Vector2 smallestAxis, ref float smallestOverlap)
        {
            // Set the local variables
            Vector2 localSmallestAxis = Vector2.Zero;
            float localSmallestOverlap = 999;

            // Grab the axis of the first asset
            IList<Vector2> axies = GetAxies(asset1Points);

            // loop through each axis that was generated
            for (int i = 0; i < axies.Count; i++)
            {
                // get the axis that we want to project against
                Vector2 axis = axies[i];

                // project the point onto both objects
                Tuple<float, float> p1 = Project(axis, asset1Points);
                Tuple<float, float> p2 = Project(axis, asset2Points);


                // If the MIN on projection2 - MAX on projection1 is GREATER than 0
                // there is no collision
                float overlap;

                if (p1.Item1 < p2.Item1)
                {
                    overlap = p2.Item1 - p1.Item2;
                }
                else
                {
                    overlap = p1.Item1 - p2.Item2;
                }
                
                if (overlap >= 0)
                    return false;
                else
                {
                    // If it is less than 0 there is a collision
                    // Get the current overlap

                    // If the current is less than the smallest set the smallest
                    // overlap and axis to the current ones
                    if (overlap > localSmallestOverlap || localSmallestOverlap == 999)
                    {
                        localSmallestOverlap = overlap;
                        localSmallestAxis = axis;
                    }
                }
            }

            if (smallestOverlap < localSmallestOverlap || smallestOverlap == 999)
            {
                // update the smallest axis and smallest overlap
                smallestAxis = localSmallestAxis;
                smallestOverlap = localSmallestOverlap;
            }
            return true;
        }

        /// <summary>
        /// Gets the axies points for each edge
        /// </summary>
        /// <param name="assetPoints">The points that we want to generate the axis for</param>
        /// <returns></returns>
        public IList<Vector2> GetAxies(IList<Vector2> assetPoints)
        {
            // Create a list for the axies's
            IList<Vector2> axies = new List<Vector2>();
            // loop through each point
            for (int i = 0; i < assetPoints.Count; i++)
            {
                // the axis is the angular vector of the compute ege/axis
                Vector2 axis;

                // Get the vector2 axis that the for the shape by doing a subtraction
                // of each vector 2 "point" that makes up the shape including the first from
                // the last
                if (i + 1 == assetPoints.Count)
                    axis = assetPoints[i] - assetPoints[0];
                else
                    axis = assetPoints[i] - assetPoints[i + 1];

                // We need to get the normal of the axis by getting the perpindicular
                // vector
                axis = new Vector2(axis.Y, -axis.X);   

                // We then need to turn it into a unit vector with a magintiue of 1
                axis.Normalize();

                // Add the axis to the list 
                axies.Add(axis);
            }

            // return all the axis's that were generated
            return axies;
        }

        /// <summary>
        /// Projects a single point against a list of points
        /// </summary>
        /// <param name="point">The singular point</param>
        /// <param name="points">The list of points</param>
        /// <returns/>The min and max projections of the point<returns>
        public Tuple<float, float> Project(Vector2 axis, IList<Vector2> points)
        {
            // to project you have to get the dot value of the single 
            // point of one other object combined with the values of each of this
            // objects points then take the min and max value
            float min = Vector2.Dot(axis, points[0]);
            float max = min;

            // the axis the the directional vector
            // the point is the maginitue vector (length)

            // Loop through each "point" of the asset
            for (int i = 0; i < points.Count; i++)
            {
                // Calculate the dot product of the axis against the current point
                // The dot product will "project" the point against the axis to form
                // a "shadow" the dot is calculated by axisX * pointX + axisY * pointY
                float dot = Vector2.Dot(axis, points[i]);

                // Grab the max and min of the projection
                if (dot < min)
                    min = dot;
                if (dot > max)
                    max = dot;
            }

            // return the projections min and max
            return Tuple.Create(min, max);
        }
    }
}