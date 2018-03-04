using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NanoEngine.ObjectTypes.LevelContents
{
    class Layer
    {
        // Holds the array of id's mathcing tiles
        [JsonProperty("data")]
        public IList<int> Data { get; set; }

        // Holds the layer name
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
