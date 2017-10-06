using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NanoEngine.ObjectTypes.Assets
{
    public abstract class Tile : ITile
    {
        private Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            protected set { texture = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Rectangle bounds;

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public void Initilise(Rectangle pos, Vector2 tilePos)
        {
            bounds = pos;
            position = tilePos;
        }
    }
}
