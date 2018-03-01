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
        private IDictionary<int, Type> tiles;

        //RootObject to hold the structure and information on tiles
        private RootObject rootObject;

        // List to hold the tiles created tiles
        private IList<ITile> generatedTiles;

        public TileManager()
        {
            tiles = new Dictionary<int, Type>();
            generatedTiles = new List<ITile>();
        }

        /// <summary>
        /// Adds a tile to the possible tiles avaliable for use
        /// </summary>
        /// <param name="tile">The tile object</param>
        public void AddTile<T>(int id) where T : ITile
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
            rootObject = (RootObject)s.Deserialize(file, typeof(RootObject));
          
            foreach (Layer layer in rootObject.layers)
            {
                if(layer.type == "tilelayer")
                {

                     int x = 0;
                     int y = 0;

                     for (var i = 0; i < layer.data.Count; i++)
                     {
                         if (i % 100 == 0)
                         {
                             y += 1;
                             x = 0;
                         }
                         x++;
                         if (layer.data[i] != 0)
                         {
                             ITile newTile = (ITile)Activator.CreateInstance(tiles[layer.data[i]]);
                             Rectangle location = new Rectangle(x * 64, (64 * rootObject.height) + (y * 64), rootObject.tileheight, rootObject.tilewidth);
                             Console.WriteLine(location);
                             newTile.Initilise(location, new Vector2(location.X, location.Y));
                             generatedTiles.Add(newTile);
                         }
                     } 
                } else
                {
                    foreach(Object obj in layer.objects)
                    {
                        Console.WriteLine("testman " + obj.properties.mind);
                    }
                }
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
            rootObject = null;
            generatedTiles = null;
        }
    }

    public class Properties
    {
        public string mind { get; set; }
    }

    public class Propertytypes
    {
        public string mind { get; set; }
    }

    public class Object
    {
        public int height { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Properties properties { get; set; }
        public Propertytypes propertytypes { get; set; }
        public int rotation { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
        public int width { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Layer
    {
        public IList<int> data { get; set; }
        public int height { get; set; }
        public string name { get; set; }
        public int opacity { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
        public int width { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public string draworder { get; set; }
        public IList<Object> objects { get; set; }
    }

    public class Tileset
    {
        public int firstgid { get; set; }
        public string source { get; set; }
    }

    public class RootObject
    {
        public int height { get; set; }
        public bool infinite { get; set; }
        public IList<Layer> layers { get; set; }
        public int nextobjectid { get; set; }
        public string orientation { get; set; }
        public string renderorder { get; set; }
        public string tiledversion { get; set; }
        public int tileheight { get; set; }
        public IList<Tileset> tilesets { get; set; }
        public int tilewidth { get; set; }
        public string type { get; set; }
        public int version { get; set; }
        public int width { get; set; }
    }

 

}
