using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Collision
{
    public class CollisionResponse
    {
        public CollisionResponse(IAsset asset)
        {
            Asset = asset;
        }

        public IAsset Asset { get; }


    }
}
