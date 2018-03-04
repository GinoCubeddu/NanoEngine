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
        private Texture2D _texture;

        public IAnimation AssetAnimation { get; protected set; }

        //getter for the texture and setter for the texture
        public Texture2D Texture { get { return _texture; } }

        public Vector2 _textureCenter;

        public int Speed;

        public IList<Vector2> Points { get; protected set; }

        private int _assetWidth;

        private int _assetHeight;

        /// <summary>
        /// Method that sets the tectire of the entity
        /// </summary>
        /// <param name="texture">Texture that the entity will use</param>
        public void SetTexture(Texture2D texture)
        {
            SetTexture(texture, texture.Width, texture.Height);
        }

        /// <summary>
        /// Method to set the texture of the entity
        /// </summary>
        /// <param name="texture">The texture to assign to the entity</param>
        /// <param name="width">The width of the texture</param>
        /// <param name="height">The height of the texure</param>
        public void SetTexture(Texture2D texture, int width, int height)
        {
            this._texture = texture;
            _assetWidth = width;
            _assetHeight = height;
            CreateBounds(_assetWidth, _assetHeight);
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
        private Vector2 _position;

        //getter and setter for the position
        public Vector2 Position { get { return _position; } set { _position = value; } }
        /// <summary>
        /// Sets the position of the entity
        /// </summary>
        /// <param name="position">Vector2 containg the position</param>
        public void SetPosition(Vector2 position)
        {
            _position = position;
            //else
            //{
            //    _position.X += position.X;
            //    _position.Y += position.Y;

            //    if (Points != null)
            //        for (int i = 0; i < Points.Count; i++)
            //        {
            //            Points[i] = new Vector2(Points[i].X + position.X, Points[i].Y + position.Y);
            //        }
            //}
            UpdateBounds();
        }

        //Field for bounds rectangle
        private Rectangle _bounds;

        //getter and setter for bounds 
        public Rectangle Bounds { get { return _bounds; } set { _bounds = value; } }

        /// <summary>
        /// Create a bounding box for the entity
        /// </summary>
        protected void CreateBounds(int width, int height)
        {
            _bounds = new Rectangle((int)Position.X, (int)Position.Y, width, height);
            _assetWidth = width;
            _assetHeight = height;
            _textureCenter = new Vector2(
                Position.X + (_assetWidth / 2), Position.Y + (_assetHeight / 2)
            );
        }

        /// <summary>
        /// Update the position of the bounds rectangle
        /// </summary>
        public void UpdateBounds()
        {
            _bounds.X = (int)Position.X;
            _bounds.Y = (int)Position.Y;
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

        /// <summary>
        /// Returns a list of points generated from the current bounds
        /// </summary>
        /// <returns>A list of points</returns>
        public IList<Vector2> GetPointsFromBounds()
        {
            IList<Vector2> pointList = new List<Vector2>();
            pointList.Add(new Vector2(_bounds.X, _bounds.Y));
            pointList.Add(new Vector2(_bounds.X, _bounds.Y + _bounds.Height));
            pointList.Add(new Vector2(_bounds.X + _bounds.Width, _bounds.Y + _bounds.Height));
            pointList.Add(new Vector2(_bounds.X + _bounds.Width, _bounds.Y));
            return pointList;
        }

        protected void AddPoint(Vector2 point)
        {
            if (Points == null)
                Points = new List<Vector2>();

            Points.Add(new Vector2(_textureCenter.X + point.X, _textureCenter.Y + point.Y));
        }
    }
}
