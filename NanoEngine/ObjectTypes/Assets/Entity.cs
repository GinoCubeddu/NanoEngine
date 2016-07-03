using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        //getter for the texture and setter for the texture
        public Texture2D Texture { get { return texture; } }

        /// <summary>
        /// Method to set the texture of the entity
        /// </summary>
        /// <param name="_texture">The texture to assign to the entity</param>
        public void setTexture(Texture2D _texture)
        {
            texture = _texture;
        }

        //Field for unique name, getter for unique name
        private string uniqueName;
        public string UniqueName { get { return uniqueName; } }

        //Field for unique id, getter for the unique id
        private int uniqueID;
        public int UniqueID { get { return uniqueID; } }

        /// <summary>
        /// setter for the unique name and id
        /// </summary>
        /// <param name="name">String for Unique Name</param>
        /// <param name="ID">String for unique ID</param>
        public void setUniqueData(string name, int ID)
        {
            uniqueName = name;
            uniqueID = ID;
        }

        //Field for position
        private Vector2 position;

        //getter and setter for the position
        public Vector2 Position { get { return position; } set { position = value; } }
        /// <summary>
        /// Sets the position of the entity
        /// </summary>
        /// <param name="ePosition">Vector2 containg the position</param>
        public void setPosition(Vector2 ePosition)
        {
            position = ePosition;
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
            bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
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
    }
}
