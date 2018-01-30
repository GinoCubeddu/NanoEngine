using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine
{
    class DirtTile : Tile
    {
        public DirtTile()
        {
            Texture = ContentManagerLoad.Manager.LoadResource<Texture2D>("Dirt_Tile");
        }
    }
}
