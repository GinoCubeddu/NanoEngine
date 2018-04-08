using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Core.Interfaces
{
    public interface IRenderManager
    {
        /// <summary>
        /// Method to change the background colour of the screen
        /// </summary>
        /// <param name="color"></param>
        void ChangeBackgroundColor(Color color);

        GameTime gameTime { get; }

        Texture2D BlankTexture { get; }

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="entity">entity to be drawn</param>
        /// <param name="tint">Color overlay for object</param>
        void Draw(Color tint);

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="position">Position of the object</param>
        /// <param name="tint">Color overlay for the object</param>
        void Draw(Texture2D texture, Vector2 position, Color tint);

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="position">Position of the object</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture</param>
        /// <param name="tint">Color overlay for the objetc</param>
        void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color tint);

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="destination">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite</param>
        /// <param name="tint">Colour overlay for the object</param>
        void Draw(Texture2D texture, Rectangle destination, Color tint);

        /// <summary>
        /// Draws an object to the screen
        /// </summary>
        /// <param name="texture">Texture for the object</param>
        /// <param name="destination">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture</param>
        /// <param name="tint">Colour overlay for the object</param>
        void Draw(Texture2D texture, Rectangle destination, Nullable<Rectangle> sourceRectangle, Color tint);

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
        void Draw(Texture2D texture, Rectangle destination, Nullable<Rectangle> sourceRectangle, Color tint, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth);

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
        void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color tint, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

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
        void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color tint, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);

        /// <summary>
        /// Draws a string to the screen
        /// </summary>
        /// <param name="font">The font to be used</param>
        /// <param name="text">The text to be drawn</param>
        /// <param name="position">The position to be drawn at</param>
        /// <param name="color">The Color of the text</param>
        void DrawText(SpriteFont font, string text, Vector2 position, Color color);

        /// <summary>
        /// Draws a line between 2 points
        /// </summary>
        /// <param name="point1">The starting point</param>
        /// <param name="point2">The destination point</param>
        /// <param name="color">The color of the line</param>
        /// <param name="width">the width of the line</param>
        void DrawLine(Vector2 point1, Vector2 point2, Color color, int width);

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        void StartDraw();

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        /// <param name="sortMode">Sprite drawing order</param>
        /// <param name="blendState">Blending options</param>
        void StartDraw(SpriteSortMode sortMode, BlendState blendState);

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        /// <param name="sortMode">Sprite drawing order</param>
        /// <param name="blendState">Blending otions</param>
        /// <param name="samplerState">Texture sampling options</param>
        /// <param name="depthStencilState">Depth and stencil options</param>
        /// <param name="rasterizerState">Rasterization options</param>
        void StartDraw(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState);

        /// <summary>
        /// Starts the sprite batch draw method
        /// </summary>
        /// <param name="sortMode">Sprite drawing order</param>
        /// <param name="blendState">Blending otions</param>
        /// <param name="samplerState">Texture sampling options</param>
        /// <param name="depthStencilState">Depth and stencil options</param>
        /// <param name="rasterizerState">Rasterization options</param>
        /// <param name="effect">Effect options</param>
        void StartDraw(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect);

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
        void StartDraw(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix);

        /// <summary>
        /// Ends the sprite batch draw method
        /// </summary>
        void EndDraw();


        /// <summary>
        /// Draws a line between 2 points
        /// </summary>
        /// <param name="point1">The starting point</param>
        /// <param name="point2">The destination point</param>
        /// <param name="color">The color of the line</param>
        /// <param name="width">the width of the line</param>
        void DrawLine(Vector2 point1, Vector2 point2, Color color, int width);

        /// <summary>
        /// Getter to return the current graphics device
        /// </summary>
        GraphicsDevice GetGD { get; }

        /// <summary>
        /// Getter to get the current scale to draw sprites at
        /// </summary>
        Vector2 SpriteScale { get; }

        /// <summary>
        /// Method that will update the current sprite scale
        /// </summary>
        void UpdateSpriteScale();
    }
}
