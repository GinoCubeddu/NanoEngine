using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Physics
{
    public class PhysicsManager : IPhysicsManager
    {

        private static IDictionary<Type, Action<IAsset, IAsset, Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs>>> _physicsMethods =
            new Dictionary<Type, Action<IAsset, IAsset, Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs>>>();


        /// <summary>
        /// Updates the physics on all the assets that are passed in
        /// </summary>
        /// <param name="assets">A list of assets to update phsyics on</param>
        public void UpdatePhysics(IList<IAsset> assets)
        {
            foreach (IAsset asset in assets)
            {
                if (!(asset is PhysicsEntity))
                    continue;
                PhysicsEntity pAsset = ((PhysicsEntity)asset);
                pAsset.Velocity *= pAsset.Damping;
                pAsset.Velocity += pAsset.Acceleration;
                pAsset.Position += pAsset.Velocity;
                pAsset.Acceleration = pAsset.Gravity;
                asset.UpdateBounds();
            }
        }

        /// <summary>
        /// checks matching types/interfaces on assets that are passed in
        /// </summary>
        /// <param name="asset">the 1st required asset</param>
        /// <param name="asset2">the 2nd required asset</param>
        public void ProcessPhysics(IAsset asset, IAsset asset2, Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs> eventArgs)
        {
            //if physics entity
            //check dict
            //
            //iterate through the keys (interfaces)
            foreach (Type type in _physicsMethods.Keys)
            {
                //if either assett has a matching key (interface)
                if (type.IsInstanceOfType(asset) || type.IsInstanceOfType(asset2))
                {
                    //then get the type (interface) and involk the method on both assets, which executes the method in the associated thread
                    _physicsMethods[type].Invoke(asset, asset2, eventArgs);
                }
            }
        }

        /// <summary>
        /// adds the type and method to the dictionary
        /// </summary>
        /// <param name="type">The interface type</param>
        /// <param name="method">injects Interfaces and methods into the dictionary</param>
        public static void Inject(Type type, Action<IAsset, IAsset, Tuple<NanoCollisionEventArgs, NanoCollisionEventArgs>> method)
        {
            //add Interface and method to the PhysicsMethods dictionary 
            _physicsMethods.Add(type, method);
        }
    }
}
