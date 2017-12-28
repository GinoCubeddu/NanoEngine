using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine
{
    public class DirtTile : Tile
    {
        public DirtTile()
        {
            Texture = ContentManagerLoad.Manager.GetTexture("Dirt_Tile");
        }
    }
}
