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
            float smallestOverlap = 1;

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
                    if (
                        !CheckOverLap(asset1Points[asset1PointKey], asset2Points[asset2PointKey], ref smallestAxis, ref smallestOverlap) ||
                        !CheckOverLap(asset2Points[asset2PointKey], asset1Points[asset1PointKey], ref smallestAxis, ref smallestOverlap)
                    )
                    {
                        // If either of them return false then there is no collision
                        collided = false;
                    } else
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
                // Set the background color to green
                RenderManager.bgColor = Color.Green;
                Console.WriteLine("overlap");
                // output the MTV
                Console.WriteLine(smallestAxis * smallestOverlap);
            }

            //// If either of the overlap checks return false then there is no overlap
            //if (!CheckOverLap(asset1Points, asset2Points) || !CheckOverLap(asset2Points, asset1Points))
            //    return null;
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
            for (int i = 0; i < axies.Count; i++)
            {
                // get the point
                Vector2 point = axies[i];

                // project the point onto both objects
                Tuple<float, float> p1 = Project(point, asset1Points);
                Tuple<float, float> p2 = Project(point, asset2Points);

                // If the MIN on projection2 - MAX on projection1 is GREATER than 0
                // there is no collision
                if (Math.Max(p1.Item1, p2.Item1) > Math.Min(p1.Item2, p2.Item2))
                    return false;
                else
                {
                    // If it is less than 0 there is a collision
                    // Get the current overlap
                    float overlap = p2.Item1 - p1.Item2;

                    // If the current is less than the smallest set the smallest
                    // overlap and axis to the current ones
                    if (overlap > localSmallestOverlap || localSmallestOverlap == 999)
                    {
                        localSmallestOverlap = overlap;
                        localSmallestAxis = point;
                    }
                }
            }

            smallestAxis = localSmallestAxis;
            smallestOverlap = localSmallestOverlap;
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
                // the edge is the vector point taken away from the next vector point
                Vector2 edge;

                if (i + 1 == assetPoints.Count)
                    edge = assetPoints[i] - assetPoints[0];
                else
                    edge = assetPoints[i] - assetPoints[i + 1];

                edge.Normalize();
                axies.Add(edge);
            }

            return axies;
        }

        /// <summary>
        /// Projects a single point against a list of points
        /// </summary>
        /// <param name="point">The singular point</param>
        /// <param name="points">The list of points</param>
        /// <returns/>The min and max projections of the point<returns>
        public Tuple<float, float> Project(Vector2 point, IList<Vector2> points)
        {
            // to project you have to get the dot value of the single 
            // point of one other object combined with the values of each of this
            // objects points then take the min and max value
            float min = Vector2.Dot(point, points[0]);
            float max = min;

            for (int i = 0; i < points.Count; i++)
            {
                float dot = Vector2.Dot(point, points[i]);
                if (dot < min)
                    min = dot;
                if (dot > max)
                    max = dot;
            }

            return Tuple.Create(min, max);
        }
    }
}