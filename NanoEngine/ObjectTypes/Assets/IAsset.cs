using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Animation;

namespace NanoEngine.ObjectTypes.Assets
{
    public interface IAsset
    {
        // Getter for the texture of the entity
        Texture2D Texture { get; }

        float DrawLevel { get; }

        // Property to state if the asset wants to be drawn/updated
        bool Despawn { get; set; }

        // getter for the position
        Vector2 Position { get; set; }

        // getter for the entitys bounding box
        Rectangle Bounds { get; set; }

        // Getter for the SpriteEffects used in drawing
        SpriteEffects AssetSpriteEffects { get; set; }

        // Transparancy setting for the asset
        float Transparancy { get; set; }

        // Getter for the points that make up an object
        IDictionary<string, IList<Vector2>> Points { get; }

        //Getter for the unique name
        string UniqueName { get; }

        //getter for the remove variable
        bool Remove { get; }

        void Move(Vector2 amount);

        // Getter to tell if the asset is moveable
        bool IsMovable { get; }

        // Getter for the animation class that belongs to the asset
        IAnimation AssetAnimation { get; }

        Vector2 BoundsOffset { get; set; }

        /// <summary>
        /// Sets the position of the entity
        /// </summary>
        /// <param name="position">Vector2 containg the position</param>
        void SetPosition(Vector2 position);

        /// <summary>
        /// setter for the unique name and id
        /// </summary>
        /// <param name="name">String for Unique Name</param>
        void SetUniqueData(string name);

        /// <summary>
        /// Method that sets the tectire of the entity
        /// </summary>
        /// <param name="texture">Texture that the entity will use</param>
        void SetTexture(Texture2D texture);

        /// <summary>
        /// Method that sets the tectire of the entity
        /// </summary>
        /// <param name="texture">Texture that the entity will use</param>
        /// <param name="width">The width of the texture</param>
        /// <param name="height">The height of the texure</param>
        void SetTexture(Texture2D texture, int width, int height);

        /// <summary>
        /// Method to update the bounds of the entity
        /// </summary>
        void UpdateBounds();

        /// <summary>
        /// Method to Initalise the entity
        /// </summary>
        void Initilise();

        /// <summary>
        /// Method rendermanager calls to draw the entity
        /// </summary>
        /// <param name="renderManager">An instance of the rendermanager</param>
        void Draw(IRenderManager renderManager);

        /// <summary>
        /// Returns a list of points generated from the current bounds
        /// </summary>
        /// <returns>A list of points</returns>
        IList<Vector2> GetPointsFromBounds();

        void Rotate(Vector2 origin, float amount);
    }
}
