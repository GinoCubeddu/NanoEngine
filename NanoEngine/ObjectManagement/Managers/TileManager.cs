using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using Newtonsoft.Json;

namespace NanoEngine.ObjectManagement.Managers
{
    class TileManager : ITileManager
    {
        private static IDictionary<string, Type> tiles;

        private static ITileManager manager;

        private static TileMap tileMap;

        private static IList<ITile> generatedTiles;
        
        public static ITileManager Manager
        {
            get { return manager ?? (manager = new TileManager()); }
        }

        private TileManager()
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
        public void DrawTileMap()
        {
            foreach(ITile tile in generatedTiles)
            {
                RenderManager.Manager.Draw(tile.Texture, tile.Bounds, Color.White);
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

            foreach (TileInformation tile in tileMap.tiles)
            {
                ITile newTile = (ITile)Activator.CreateInstance(tiles[tile.spriteName]);
                Rectangle location = new Rectangle(tile.x * 64, tile.y * 64, 64, 64);
                newTile.Initilise(location, new Vector2(tile.x, tile.y));
                generatedTiles.Add(newTile);
            }
        }
    }

    public class TileMap
    {
        public string levelName { get; set; }
        public IList<int> playerSpawn { get; set; }
        public IList<TileInformation> tiles { get; set; }
    }

    public class TileInformation
    {
        public int x { get; set; }
        public int y { get; set; }
        public string spriteName { get; set; }
    }
}
