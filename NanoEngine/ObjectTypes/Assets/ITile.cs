using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace NanoEngine.ObjectTypes.Assets
{
    public interface ITile : IAsset
    {
        void Initilise(Rectangle pos, Vector2 tilePos);
    }
}
