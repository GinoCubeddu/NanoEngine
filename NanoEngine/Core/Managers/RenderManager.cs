using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Locator;

namespace NanoEngine.Core.Managers
{
    public class RenderManager : GameComponent, IRenderManager
    {
        public static Rectangle RenderBounds { get; private set; }
        
        //private static field to hold the instace of Game1
        private static Game _game1;

        public GameTime gameTime { get; private set; }

        //Private field to hold refrence to sprite batch
        private static SpriteBatch spriteBatch;

        private IDictionary<Color, Texture2D> _blankTextures;

        // Blank texture used for drwaing items such as lines
        private Texture2D _blankTexture;

        public Texture2D BlankTexture
        {
            get { return _blankTexture; }
        }

        private Vector2 spriteScale;

        /// <summary>
        /// Getter to get the current scale to draw sprites at
        /// </summary>
        public Vector2 SpriteScale
        {
            get { return spriteScale; }
        }
        
        //private field to hold background colour
        public static Color bgColor;

        // private field used to make sure only one instace of the manager is allowed
        private static bool Created;

        /// <summary>
        /// Constructor for the renderer
        /// </summary>
        /// <param name="game">The game that the manager will be rendering</param>
        public RenderManager(Game game)
            : base(game)
        {
            if (Created)
                throw new Exception("Only one instance of the RenderManager may be created");
            _game1 = game;
            RenderBounds = GetGD.Viewport.Bounds;
            RenderBounds = GetGD.Viewport.Bounds;
            _blankTexture = new Texture2D(GetGD, 1, 1);
            _blankTexture.SetData<Color>(new Color[] { Color.Black });
            _blankTextures = new Dictionary<Color, Texture2D>();
            _blankTextures[Color.Black] = _blankTexture;
            Created = true;
        }

        /// <summary>
        /// Method to change the background colour of the screen
        /// </summary>
        /// <param name="color"></param>
        public void ChangeBackgroundColor(Color color)
        {
            bgColor = color;
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="entity">entity to be drawn</param>
        /// <param name="tint">Color overlay for object</param>
        public void Draw(Color tint)
        {
            ////Console.WriteLine(tint);
            //spriteBatch.Draw(obj.Texture, obj.Position, tint);
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="position">Position of the object</param>
        /// <param name="tint">Color overlay for the object</param>
        public void Draw(Texture2D texture, Vector2 position, Color tint)
        {
            spriteBatch.Draw(texture, position, tint);
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="position">Position of the object</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture</param>
        /// <param name="tint">Color overlay for the objetc</param>
        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color tint)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, tint);
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="destination">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite</param>
        /// <param name="tint">Colour overlay for the object</param>
        public void Draw(Texture2D texture, Rectangle destination, Color tint)
        {
            spriteBatch.Draw(texture, destination, tint);
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="destination">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture</param>
        /// <param name="tint">Colour overlay for the object</param>
        public void Draw(Texture2D texture, Rectangle destination, Nullable<Rectangle> sourceRectangle, Color tint)
        {
            spriteBatch.Draw(texture, destination, sourceRectangle, tint);
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="destination">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture</param>
        /// <param name="tint">Colour overlay for the object</param>
        /// <param name="rotation">The rotation for the object to rotate around the origin</param>
        /// <param name="origin">The origin of the shape</param>
        /// <param name="effects">Effects to apply</param>
        /// <param name="layerDepth">The depth of the layer</param>
        public void Draw(Texture2D texture, Rectangle destination, Nullable<Rectangle> sourceRectangle, Color tint, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, destination, sourceRectangle, tint, rotation, origin, effects, layerDepth);
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="destination">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture</param>
        /// <param name="tint">Colour overlay for the object</param>
        /// <param name="rotation">The rotation for the object to rotate around the origin</param>
        /// <param name="origin">The origin of the shape</param>
        /// <param name="scale">The scale of the object to be drawn</param>
        /// <param name="effects">Effects to apply</param>
        /// <param name="layerDepth">The depth of the layer</param>
        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color tint, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, tint, rotation, origin, scale, effects, layerDepth);
        }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="destination">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture</param>
        /// <param name="tint">Colour overlay for the object</param>
        /// <param name="rotation">The rotation for the object to rotate around the origin</param>
        /// <param name="origin">The origin of the shape</param>
        /// <param name="scale">The scale of the object to be drawn</param>
        /// <param name="effects">Effects to apply</param>
        /// <param name="layerDepth">The depth of the layer</param>
        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color tint, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, tint, rotation, origin, scale, effects, layerDepth);
        }

        /// <summary>
        /// Draws a string to the screen
        /// </summary>
        /// <param name="font">The font to be used</param>
        /// <param name="text">The text to be drawn</param>
        /// <param name="position">The position to be drawn at</param>
        /// <param name="color">The Color of the text</param>
        public void DrawText(SpriteFont font, string text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, text, position, color);
        }

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        public void StartDraw()
        {
            spriteBatch.Begin();
        }

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        /// <param name="sortMode">Sprite drawing order</param>
        /// <param name="blendState">Blending options</param>
        public void StartDraw(SpriteSortMode sortMode, BlendState blendState)
        {
            spriteBatch.Begin(sortMode, blendState);
        }

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        /// <param name="sortMode">Sprite drawing order</param>
        /// <param name="blendState">Blending otions</param>
        /// <param name="samplerState">Texture sampling options</param>
        /// <param name="depthStencilState">Depth and stencil options</param>
        /// <param name="rasterizerState">Rasterization options</param>
        public void StartDraw(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState)
        {
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState);
        }

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        /// <param name="sortMode">Sprite drawing order</param>
        /// <param name="blendState">Blending otions</param>
        /// <param name="samplerState">Texture sampling options</param>
        /// <param name="depthStencilState">Depth and stencil options</param>
        /// <param name="rasterizerState">Rasterization options</param>
        /// <param name="effect">Effect options</param>
        public void StartDraw(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect)
        {
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect);
        }

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        /// <param name="sortMode">Sprite drawing order</param>
        /// <param name="blendState">Blending otions</param>
        /// <param name="samplerState">Texture sampling options</param>
        /// <param name="depthStencilState">Depth and stencil options</param>
        /// <param name="rasterizerState">Rasterization options</param>
        /// <param name="effect">Effect options</param>
        /// <param name="transformMatrix">Transformation matrix for scale, rotate, translate options.</param>
        public void StartDraw(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
        {
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        /// <summary>
        /// Ends the sprite batch draw method
        /// </summary>
        public void EndDraw()
        {
            spriteBatch.End();
        }

        /// <summary>
        /// Update method where main drawing happens
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            //Sets background color
            _game1.GraphicsDevice.Clear(bgColor);

            ServiceLocator.Instance.RetriveService<ISceneManager>(DefaultNanoServices.SceneManager).Draw(this);
        }

        /// <summary>
        /// Method to load the initial content
        /// </summary>
        protected void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(_game1.GraphicsDevice);
            bgColor = Color.CornflowerBlue;
            Matrix _scale = Matrix.CreateScale(_game1.GraphicsDevice.Viewport.Width / 800f, _game1.GraphicsDevice.Viewport.Width / 800f, 1);
            spriteScale = new Vector2(_scale.Scale.X, _scale.Scale.Y);
        }

        /// <summary>
        /// Method that will update the current sprite scale
        /// </summary>
        public void UpdateSpriteScale()
        {
            Matrix _scale = Matrix.CreateScale(_game1.GraphicsDevice.Viewport.Width / 800f, _game1.GraphicsDevice.Viewport.Width / 800f, 1);
            spriteScale = new Vector2(_scale.Scale.X, _scale.Scale.Y);
        }

        /// <summary>
        /// Getter to return the current graphics device
        /// </summary>
        public GraphicsDevice GetGD { get { return _game1.GraphicsDevice; } }

        /// <summary>
        /// Draws a line between 2 points
        /// </summary>
        /// <param name="point1">The starting point</param>
        /// <param name="point2">The destination point</param>
        /// <param name="color">The color of the line</param>
        /// <param name="width">the width of the line</param>
        public void DrawLine(Vector2 point1, Vector2 point2, Color color, int width)
        {
            Texture2D blankTexture = null;
            if (_blankTextures.ContainsKey(color))
                blankTexture = _blankTextures[color];
            else
            {
                _blankTextures[color] = blankTexture = new Texture2D(GetGD, 1, 1);
                _blankTextures[color].SetData<Color>(new Color[] { color });
                blankTexture = _blankTextures[color];
            }

            Vector2 edge = point2 - point1;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            float distance;
            Vector2.Distance(ref point1, ref point2, out distance);
            Draw(
                blankTexture,
                point1,
                new Rectangle((int)point1.X, (int)point1.Y, (int)distance, width),
                color,
                angle,
                new Vector2(0, 0),
                1,
                SpriteEffects.None,
                0
            );
        }

        /// <summary>
        /// Method to initialise everything
        /// </summary>
        public override void Initialize()
        {
            LoadContent();
        }    
    }
}
