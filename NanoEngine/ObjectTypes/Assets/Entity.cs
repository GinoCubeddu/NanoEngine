using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Assets
{
    public abstract class Entity : IEntity
    {
        //Feild for holding the texture
        private Texture2D texture;

        public Animation AssetAnimation { get; protected set; }

        //getter for the texture and setter for the texture
        public Texture2D Texture { get { return texture; } }

        /// <summary>
        /// Method to set the texture of the entity
        /// </summary>
        /// <param name="texture">The texture to assign to the entity</param>
        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        //Field for unique name, getter for unique name
        private string uniqueName;
        public string UniqueName { get { return uniqueName; } }

        /// <summary>
        /// setter for the unique name and id
        /// </summary>
        /// <param name="name">String for Unique Name</param>
        /// <param name="ID">String for unique ID</param>
        public void SetUniqueData(string name)
        {
            uniqueName = name;
        }

        //Field for position
        private Vector2 position;

        //getter and setter for the position
        public Vector2 Position { get { return position; } set { position = value; } }
        /// <summary>
        /// Sets the position of the entity
        /// </summary>
        /// <param name="position">Vector2 containg the position</param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
            CreateBounds();
        }

        //Field for bounds rectangle
        private Rectangle bounds;

        //getter and setter for bounds 
        public Rectangle Bounds { get { return bounds; } set { bounds = value; } }

        /// <summary>
        /// Create a bounding box for the entity
        /// </summary>
        protected void CreateBounds()
        {
            // bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        /// <summary>
        /// Update the position of the bounds rectangle
        /// </summary>
        public void UpdateBounds()
        {
            bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            //bounds.X = (int)Position.X;
            //bounds.Y = (int)Position.Y;
        }

        //getter for the remove property
        private bool remove;
        public bool Remove { get { return remove; } }

        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public abstract void Initilise();

        public virtual void Draw(IRenderManager renderManager)
        {
            if (AssetAnimation != null)
                AssetAnimation.Animate(renderManager);
            else
                renderManager.Draw(Texture, Position, Color.White);
        }
    }
}
