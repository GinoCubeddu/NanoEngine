using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Assets
{
    public interface IAsset
    {
        //Getter for the texture of the entity
        Texture2D Texture { get; }

        //getter for the position
        Vector2 Position { get; set; }

        //getter for the entitys bounding box
        Rectangle Bounds { get; set; }

        //Getter for the unique name
        string UniqueName { get; }

        //getter for the remove variable
        bool Remove { get; }

        // Getter for the animation class that belongs to the asset
        Animation AssetAnimation { get; }

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
    }
}
