using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Physics
{
    internal class PhysicsManager : IPhysicsManager
    {

        private static IDictionary<Type, Action<IAsset, IAsset>> PhysicsMethods = new Dictionary<Type, Action<IAsset, IAsset>>();


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
            }
        }

        /// <summary>
        /// checks matching types/interfaces on assets that are passed in
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="asset2"></param>
        public void ProcessPhysics(IAsset asset, IAsset asset2)
        {
            //if physics entity
            //check dict
            //
            //iterate through the keys (interfaces)
            foreach (Type type in PhysicsMethods.Keys)
            {
                //if either assett has a matching key (interface)
                if (type.IsInstanceOfType(asset) || type.IsInstanceOfType(asset2))
                {
                    //then get the type (interface) and involk the method on both assets, which executes the method in the associated thread
                    PhysicsMethods[type].Invoke(asset, asset2);
                }
            }
        }

        /// <summary>
        /// adds the type and method to the dictionary
        /// </summary>
        /// <param name="type">The interface type</param>
        /// <param name="method">injects Interfaces and methods into the dictionary</param>
        public static void Inject(Type type, Action<IAsset, IAsset> method)
        {
            //add Interface and method to the PhysicsMethods dictionary 
            PhysicsMethods.Add(type, method);
        }
    }
}
