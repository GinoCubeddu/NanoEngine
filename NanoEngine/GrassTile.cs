using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine
{
    class GrassTile : Tile
    {
        public GrassTile()
        {
            Texture = ContentManagerLoad.Manager.GetTexture("Grass_Tile");
        }
    }
}
