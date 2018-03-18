using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.ObjectManagement.Interfaces
{
    public interface ILevelLoader
    {
        // Provides access to the level bounds
        Rectangle LevelBounds { get; }

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
        int LoadTileMap(
            string fileName, IDictionary<string, IAsset> assets,
            IDictionary<string, IAiComponent> aiComponents, IAssetFactory assetFactory,
            IAiFactory aiFactory, int uId
        );
    }
}
