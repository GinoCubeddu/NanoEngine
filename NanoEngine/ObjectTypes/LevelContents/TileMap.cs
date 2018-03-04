using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NanoEngine.ObjectTypes.LevelContents
{
    class TileMap
    {
        // The level height
        [JsonProperty("height")]
        public int Height { get; set; }

        // the level width
        [JsonProperty("width")]
        public int Width { get; set; }

        // The layers of the level
        [JsonProperty("layers")]
        public IList<Layer> Layers { get; set; }

        // The general height size of tiles
        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }

        // The general width of tiles
        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }
    }
}
