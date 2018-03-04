using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.ObjectTypes.LevelContents;
using Newtonsoft.Json;
using NanoEngine.Testing.Assets;

namespace NanoEngine.ObjectManagement.Managers
{
    class LevelLoader : ILevelLoader
    {
        private static IDictionary<int, Tuple<Type, Type, string>> _possibleAssets;

        /// <summary>
        /// Adds the tile type of T to the possible tile list
        /// </summary>
        /// <typeparam name="T">The asset type to be added</typeparam>
        /// <param name="id">The id that the asset type will be assigned</param>
        public static void AddLevelAsset<T>(int id) where T : IAsset
        {
            AddLevelAsset(id, typeof(T), null, null);
        }

        /// <summary>
        /// Adds the asset type of T to the possible asset list and adds the
        /// Ai asset type of U to the possible asset list
        /// </summary>
        /// <typeparam name="T">The asset type to be added</typeparam>
        /// <typeparam name="U">The Ai type to be added</typeparam>
        /// <param name="id">The id that the asset type will be assigned</param>
        public static void AddLevelAsset<T, U>(int id) where T : IAsset where U : IAiComponent
        {
            AddLevelAsset(id, typeof(T), typeof(U), null);
        }

        /// <summary>
        /// Adds the tile type of T to the possible tile list
        /// </summary>
        /// <typeparam name="T">The asset type to be added</typeparam>
        /// <param name="id">The id that the asset type will be assigned</param>
        /// <param name="uName">The unique name that this asset should have</param>
        public static void AddLevelAsset<T>(int id, string uName) where T : IAsset
        {
            AddLevelAsset(id, typeof(T), null, uName);
        }

        /// <summary>
        /// Adds the asset type of T to the possible asset list and adds the
        /// Ai asset type of U to the possible asset list
        /// </summary>
        /// <typeparam name="T">The asset type to be added</typeparam>
        /// <typeparam name="U">The Ai type to be added</typeparam>
        /// <param name="id">The id that the asset type will be assigned</param>
        /// <param name="uName">The unique name that this asset should have</param>
        public static void AddLevelAsset<T, U>(int id, string uName) where T : IAsset where U : IAiComponent
        {
            AddLevelAsset(id, typeof(T), typeof(U), uName);
        }

        /// <summary>
        /// Adds the asset and aiType to the dictonary along with the uname
        /// </summary>
        /// <typeparam name="T">The asset type to be added</typeparam>
        /// <typeparam name="U">The Ai type to be added</typeparam>
        /// <param name="id">The id that the asset type will be assigned</param>
        /// <param name="uName">The unique name that this asset should have</param>
        private static void AddLevelAsset(int id, Type assetType, Type aiType, string uName)
        {
            if (_possibleAssets == null)
                _possibleAssets = new Dictionary<int, Tuple<Type, Type, string>>();

            _possibleAssets[id] = new Tuple<Type, Type, string>(assetType, aiType, uName);
        }

        /// <summary>
        /// Loads the requested tile map assets and aiComponets into the passed in
        /// dictonaries
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="assets">A dictonary for the assets</param>
        /// <param name="aiComponents">A dictonary for the AiComonents</param>
        /// <param name="assetFactory">A assetFactory instance</param>
        /// <param name="aiFactory">A aiFactory instance</param>
        /// <param name="uId">a uId counter for asset names that have not requested a specific uName</param>
        public void LoadTileMap(
            string fileName, IDictionary<string, IAsset> assets,
            IDictionary<string, IAiComponent> aiComponents, IAssetFactory assetFactory,
            IAiFactory aiFactory, int uId
        )
        {
            // append the json extention to the file name if it is not there already
            if (!fileName.Contains(".json"))
                fileName += ".json";

            // Create  anew json reader
            JsonSerializer s = new JsonSerializer();
            // open the file and deserialize it into a TileMap object
            var file = File.OpenText("Content/" + fileName);
            TileMap tileMap = (TileMap)s.Deserialize(file, typeof(TileMap));

            foreach (Layer layer in tileMap.Layers)
            {
                int x = 0;
                int y = 0;

                for (var i = 0; i < layer.Data.Count; i++)
                {
                    // If we are at the end of the map then go down one row and
                    // reset the coloum to 0
                    if (i % tileMap.Width == 0)
                    {
                        y += 1;
                        x = 0;
                    }

                    // We dont want to add empty tiles
                    if (layer.Data[i] != 0)
                    {
                        // Create the new position for the asset
                        Vector2 position = new Vector2(x * tileMap.TileWidth, y * tileMap.TileHeight);
                        

                        Console.WriteLine(layer.Data[i]);

                        // Create a new asset
                        IAsset asset = assetFactory.RetriveNewAsset(
                            _possibleAssets[layer.Data[i]].Item1, "",  position
                        );

                        // If the asset is not quite the size of the tile size then offset it
                        // so it fits in the center
                        if (asset.Bounds.Width != tileMap.TileWidth || asset.Bounds.Height != tileMap.TileHeight)
                        {
                            Console.WriteLine(asset.UniqueName + " " + (tileMap.TileWidth - asset.Bounds.Width));
                            asset.SetPosition(new Vector2(
                                asset.Position.X + ((tileMap.TileWidth - asset.Bounds.Width) * 0.5f),
                                asset.Position.Y + (tileMap.TileHeight - asset.Bounds.Height)
                            ));
                        }
                        Console.WriteLine(asset.Bounds);

                        // Grab the requested uniqe name for the asset
                        string uName = _possibleAssets[layer.Data[i]].Item3;

                        // if the asset did not require a certian name then create one for it
                        if (uName == null)
                        {
                            uName = _possibleAssets[layer.Data[i]].Item1.ToString() + uId;
                            uId++;
                        }

                        // Add the asset to the dictonary
                        assets[uName] = asset;

                        // If the asset
                        if (_possibleAssets[layer.Data[i]].Item2 != null)
                        {
                            aiComponents[uName] = aiFactory.CreateAi(_possibleAssets[layer.Data[i]].Item2);
                            aiComponents[uName].InitialiseAiComponent(asset);
                        }
                    }
                    x++;
                }
            }
        }
    }
}
