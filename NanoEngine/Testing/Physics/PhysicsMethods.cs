using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Physics
{
    static class PhysicsMethods
    {
        public static void Bounce(IAsset asset1, IAsset asset2)
        {
            if (asset1 is IBounce)
                Console.WriteLine("Asset 1 is IBOUNCE = ");
            if (asset2 is IBounce)
                Console.WriteLine("Asset 2 is IBOUNCE = ");
        }

        public static void Reflect(IAsset asset1, IAsset asset2)
        {
            if (asset1 is IReflect)
                Console.WriteLine("Asset 1 is IREFLECT = ");
            if (asset2 is IReflect)
                Console.WriteLine("Asset 2 is IREFLECT = ");
        }
    }
}
