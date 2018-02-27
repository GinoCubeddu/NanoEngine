using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.ObjectTypes.Assets
{
    public abstract class Tile : ITile
    {
        private Texture2D texture;

        public IAnimation AssetAnimation { get; protected set; }

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

        public IList<Vector2> Points { get; }
        public string UniqueName { get; }
        public bool Remove { get; }

        /// <summary>
        /// Sets the position of the entity
        /// </summary>
        /// <param name="position">Vector2 containg the position</param>
        public void SetPosition(Vector2 position)
        {
        }

        /// <summary>
        /// setter for the unique name and id
        /// </summary>
        /// <param name="name">String for Unique Name</param>
        /// <param name="ID">String for unique ID</param>
        public void SetUniqueData(string name)
        {

        }

        /// <summary>
        /// Method that sets the tectire of the entity
        /// </summary>
        /// <param name="texture">Texture that the entity will use</param>
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }

        /// <summary>
        /// Method that sets the tectire of the entity
        /// </summary>
        /// <param name="texture">Texture that the entity will use</param>
        /// <param name="width">The width of the texture</param>
        /// <param name="height">The height of the texure</param>
        public void SetTexture(Texture2D texture, int width, int height)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to update the bounds of the entity
        /// </summary>
        public void UpdateBounds()
        {
        }

        /// <summary>
        /// Method to Initalise the entity
        /// </summary>
        public void Initilise()
        {
        }

        public void Initilise(Rectangle pos, Vector2 tilePos)
        {
            bounds = pos;
            position = tilePos;
        }

        public void Draw(IRenderManager renderManager)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of points generated from the current bounds
        /// </summary>
        /// <returns>A list of points</returns>
        public IList<Vector2> GetPointsFromBounds()
        {
            throw new NotImplementedException();
        }
    }
}
