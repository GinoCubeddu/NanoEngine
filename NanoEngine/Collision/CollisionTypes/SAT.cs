using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
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
        public bool CheckCollision(IAsset asset1, IAsset asset2)
        {
            // Get the points of the two collidables, if no points are defined use
            // points generated from the bounding box
            IList<Vector2> asset1Points = asset1.Points ?? asset1.GetPointsFromBounds();
            IList<Vector2> asset2Points = asset2.Points ?? asset2.GetPointsFromBounds();

            // If either of the overlap checks return false then there is no overlap
            if (!CheckOverLap(asset1Points, asset2Points) || !CheckOverLap(asset2Points, asset1Points))
                return false;
            return true;
        }

        /// <summary>
        /// Checks to see if 2 lists of points overlap
        /// </summary>
        /// <param name="asset1Points">The first list of points</param>
        /// <param name="asset2Points">The second list of points</param>
        /// <returns>Boolean telling us if they have overlapped</returns>
        private bool CheckOverLap(IList<Vector2> asset1Points, IList<Vector2> asset2Points)
        {
            // get the axies's from the first object
            IList<Vector2> axies = GetAxies(asset1Points);
            for (int i = 0; i < axies.Count; i++)
            {
                // get the point
                Vector2 point = axies[i];

                // project the point onto both objects
                Tuple<float, float> p1 = Project(point, asset1Points);
                Tuple<float, float> p2 = Project(point, asset2Points);

                if (p2.Item1 - p1.Item2 > 0) return false;
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
                else if (dot > max)
                    max = dot;
            }

            return Tuple.Create(min, max);
        }
    }
}