using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Assets
{
    public abstract class Entity : IAsset
    {
        //Feild for holding the texture
        private Texture2D _texture;

        public static bool DrawAssetBounds = false;

        protected Vector2 _origin = Vector2.Zero;

        public IAnimation AssetAnimation { get; protected set; }

        //getter for the texture and setter for the texture
        public Texture2D Texture { get { return _texture; } }

        public Vector2 _textureCenter;

        public int Speed;

        public IList<IList<Vector2>> Points { get; protected set; }

        private int _assetWidth;

        private int _assetHeight;

        private bool _despawn = false;

        // Property to state if the asset wants to be drawn/updated
        public bool Despawn
        {
            get { return _despawn;}
            set { _despawn = value; }
        }

        private float rotation = 0;


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
            Position = Vector2.Zero;
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

        protected void DrawBounds(IRenderManager rendermanager)
        {
            if (DrawAssetBounds)
            {
                // Draw the bounding box
                IList<Vector2> boundPoints = GetPointsFromBounds();
                for (int i = 0; i < boundPoints.Count; i++)
                    rendermanager.DrawLine(boundPoints[i], boundPoints[i + 1 == boundPoints.Count ? 0 : i + 1], Color.Black, 2);


                IList<Color> colors = new List<Color>() {Color.Pink, Color.Red, Color.Gold, Color.Yellow};

                // If there are set points then draw them
                if (Points != null)
                {
                    int color = 0;
                    foreach (IList<Vector2> points in Points)
                    {
                        for (int i = 0; i < points.Count; i++)
                        {
                            rendermanager.DrawLine(points[i], points[i + 1 == points.Count ? 0 : i + 1], colors[color], 2);
                        }
                        color++;
                        if (color >= colors.Count)
                            color = 0;
                    }
                }
                    
                        
            }
        }
        public virtual void Draw(IRenderManager renderManager)
        {
            //rotation += 0.01f;
            if (AssetAnimation != null)
                AssetAnimation.Animate(renderManager);
            else
            {
                renderManager.Draw(Texture, Position, null, Color.White, rotation, Vector2.Zero, 1, SpriteEffects.None, 1);
                CreateBounds(Texture.Width, Texture.Height);
                Console.WriteLine(Texture.Bounds.X);
                Console.WriteLine(Texture.Bounds.Y);
                

            }
            DrawBounds(renderManager);
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

        public void Rotate(Vector2 origin, float amount)
        {
            if (Points != null)
            {
                IList<IList<Vector2>> OriginPoints = new List<IList<Vector2>>(Points);
                for (int i = 0; i < Points.Count; i++)
                {
                    for (int j = 0; j < Points[i].Count; j++)
                    {
                        Matrix transform = Matrix.CreateTranslation(-origin.X, -origin.Y, 0f) *
                                           Matrix.CreateRotationZ(amount) *
                                           Matrix.CreateTranslation(origin.X, origin.Y, 0f);

                        Points[i][j] = Vector2.Transform(Points[i][j], transform);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a list of points to make a shape around the entity. The points will
        /// be placed from the center point
        /// </summary>
        /// <param name="points"></param>
        protected void AddPoints(IList<Vector2> points)
        {
            if (Points == null)
                Points = new List<IList<Vector2>>();

            IList<Vector2> shapePoints = new List<Vector2>();

            foreach (Vector2 point in points)
                shapePoints.Add(new Vector2(_textureCenter.X + point.X, _textureCenter.Y + point.Y));
            Points.Add(shapePoints);
        }
    }
}
