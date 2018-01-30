using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing
{
    class GrassTile : Tile
    {
        public GrassTile()
        {
            Texture = ContentManagerLoad.Manager.LoadResource<Texture2D>("Grass_Tile");
        }
    }
}
