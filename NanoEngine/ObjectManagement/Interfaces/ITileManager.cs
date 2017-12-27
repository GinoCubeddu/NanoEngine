using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.ObjectManagement.Interfaces
{
    public interface ITileManager
    {
        /// <summary>
        /// Adds the tile type of T to the possible tile list
        /// </summary>
        /// <typeparam name="T">The tile type to be added</typeparam>
        /// <param name="id">The id that the tyle type will have</param>
        void AddTile<T>(string id) where T : ITile;

        /// <summary>
        /// Draws the current tile map
        /// </summary>
        void DrawTileMap();

        /// <summary>
        /// Loads a new tilemap ready to be drawn
        /// </summary>
        /// <param name="fileName">The name of the json tile map file</param>
        void LoadTileMap(string fileName);

        /// <summary>
        /// Unloads all the content that the tile manager has created by
        /// setting the refrences to null makining them elidable for garbage
        /// collection. Should only be called before TileManager is about to
        /// be destroyed itsself.
        /// </summary>
        void UnloadContent();
    }
}
