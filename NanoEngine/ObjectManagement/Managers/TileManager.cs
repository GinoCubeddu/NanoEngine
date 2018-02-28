using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using Newtonsoft.Json;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.ObjectManagement.Managers
{
    class TileManager : ITileManager
    {
        // Private Dictonary to hold the types of tiles avaliable
        private IDictionary<string, Type> tiles;

        // TileMap object to hold the structure and information on tiles
        private TileMap tileMap;

        // List to hold the tiles created tiles
        private IList<ITile> generatedTiles;

        public TileManager()
        {
            tiles = new Dictionary<string, Type>();
            generatedTiles = new List<ITile>();
        }

        /// <summary>
        /// Adds a tile to the possible tiles avaliable for use
        /// </summary>
        /// <param name="tile">The tile object</param>
        public void AddTile<T>(string id) where T : ITile
        {
            if(!tiles.Values.Contains(typeof(T)))
            {
                tiles.Add(id, typeof(T));
                Console.WriteLine("added");
                return;
            }
            Console.WriteLine(
                "WARNING: " + typeof(T) + " already in tile dict " +
                "continueing without adding"
            );
        }

        /// <summary>
        /// Draws the current tile map
        /// </summary>
        /// <param name="renderManager">An instance of the render manager which will be used to draw</param>
        public void DrawTileMap(IRenderManager renderManager)
        {
            foreach(ITile tile in generatedTiles)
            {
                renderManager.Draw(tile.Texture, tile.Bounds, Color.White);
            }
        }

        /// <summary>
        /// Loads a new tilemap ready to be drawn
        /// </summary>
        /// <param name="fileName">The name of the json tile map file</param>
        public void LoadTileMap(string fileName)
        {
            if(!fileName.Contains(".json"))
            {
                fileName += ".json";
            }

            JsonSerializer s = new JsonSerializer();
            var file = File.OpenText("Content/" + fileName);
            tileMap = (TileMap)s.Deserialize(file, typeof(TileMap));
          

          foreach (TileInformation tile in tileMap.layers)
            {
                var sum = tile.data;
                Console.WriteLine("sum " + sum);
          
                ITile newTile = (ITile)Activator.CreateInstance(tiles[tile.name]);
                Rectangle location = new Rectangle(tile.x * 64, (64 * tileMap.height) - tile.y * 64, tileMap.tileheight, tileMap.tilewidth);
                newTile.Initilise(location, new Vector2(tile.x, tile.y));
                generatedTiles.Add(newTile);
               
          }
        }

        /// <summary>
        /// Unloads all the content that the tile manager has created by
        /// setting the refrences to null makining them elidable for garbage
        /// collection. Should only be called before TileManager is about to
        /// be destroyed itsself.
        /// </summary>
        public void UnloadContent()
        {
            tiles = null;
            tileMap = null;
            generatedTiles = null;
        }
    }

    public class TileMap
    {
        public int height { get; set; }
        public int tilewidth { get; set; }
        public int tileheight { get; set; }

        public IList<TileInformation> layers { get; set; }
    }

    public class TileInformation
    {

        public int height { get; set; }
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public IList<int> data { get; set; }
    }

}
