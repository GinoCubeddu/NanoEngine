using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.Assets
{
    public interface IEntity : IAsset
    {
        //Getter for the unique name
        string UniqueName { get; }

        //getter for the unique id
        int UniqueID { get; }

        //getter for the remove variable
        bool Remove { get; }

        /// <summary>
        /// Sets the position of the entity
        /// </summary>
        /// <param name="ePosition">Vector2 containg the position</param>
        void setPosition(Vector2 ePosition);

        /// <summary>
        /// setter for the unique name and id
        /// </summary>
        /// <param name="name">String for Unique Name</param>
        /// <param name="ID">String for unique ID</param>
        void setUniqueData(string name, int ID);

        /// <summary>
        /// Method that sets the tectire of the entity
        /// </summary>
        /// <param name="texture">Texture that the entity will use</param>
        void setTexture(Texture2D texture);

        /// <summary>
        /// Method to update the bounds of the entity
        /// </summary>
        void UpdateBounds();

        /// <summary>
        /// Method to Initalise the entity
        /// </summary>
        void Initilise();
    }
}
