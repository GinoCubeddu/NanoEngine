using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.ObjectManagement
{
    public class RenderFilter : IRenderFilter
    {
        public static Vector2 RenderOffset = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

        public IList<IAsset> RenderTargets = new List<IAsset>();

        /// <summary>
        /// Takes a dictonary of assets and filters out the ones that are not set in the render offset of
        /// the render targets. If no render target
        /// </summary>
        /// <param name="assets">The dictonary of assets to filter</param>
        /// <returns>A dictonary containing only the assets that are in the render zone(s)</returns>
        public IDictionary<string, IAsset> SortAssetsInRenderZone(IDictionary<string, IAsset> assets)
        {
            // If there are no render targets then no filter will be applied
            if (RenderTargets.Count == 0)
                return assets;

            // Return a dictonary with the assets not in the render zone filtered out
            return FilterDictonary<IAsset>(GetRenderableAssetNames(assets.Values.ToList()), assets);
        }

        /// <summary>
        /// Takes a dictonary of ai components and filters out the ones that are not set in the render offset of
        /// the render targets. If no render target
        /// </summary>
        /// <param name="aiComponents">The dictonary of ai components to filter</param>
        /// <returns>A dictonary containing only the assets that are in the render zone(s)</returns>
        public IDictionary<string, IAiComponent> SortAiInRenderZone(IDictionary<string, IAiComponent> aiComponents)
        {
            // If there are no render targets then no filter will be applied
            if (RenderTargets.Count == 0)
                return aiComponents;

            // Get all the assets of the ai components
            IList<IAsset> assets = new List<IAsset>();
            foreach (IAiComponent aiComponent in aiComponents.Values)
                assets.Add(aiComponent.ControledAsset);

            // Return a dictonary with the assets not in the render zone filtered out
            return FilterDictonary<IAiComponent>(GetRenderableAssetNames(assets), aiComponents);
        }

        /// <summary>
        /// Adds a render target to the RenderFilter
        /// </summary>
        /// <param name="asset">A render target for the filter to use</param>
        public void AddRenderTarget(IAsset asset)
        {
            RenderTargets.Add(asset);
        }

        /// <summary>
        /// Creates a dictonary of type T as the value and a string as its key. The dictonary will
        /// take the passed in names and return a dict with only the values of the names
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="names">The list of names</param>
        /// <param name="objectList">The original dict containing all objects of type T</param>
        /// <returns></returns>
        private IDictionary<string, T> FilterDictonary<T>(IList<string> names, IDictionary<string, T> objectList)
        {
            // Create a new dict of type T
            IDictionary<string, T> renderableAssets = new Dictionary<string, T>();

            // Loop through the passed in names
            foreach (string renderableAssetName in names)
                // If the name is within the passed in dict add it to the rendable assets
                if (objectList.ContainsKey(renderableAssetName))
                    renderableAssets[renderableAssetName] = objectList[renderableAssetName];

            return renderableAssets;
        }

        /// <summary>
        /// Loops through the passed in assets and returns all the names of the assets
        /// that fall within the "RenderZones" calculated from the render targets and the render
        /// offset
        /// </summary>
        /// <param name="assets">The list of assets to check</param>
        /// <returns>the names of all the assets within the render zones</returns>
        private IList<string> GetRenderableAssetNames(IList<IAsset> assets)
        {
            // Generate the renderOffsets for each renderAround target
            IList<IList<int>> renderOffset = new List<IList<int>>();
            for (int i = 0; i < RenderTargets.Count; i++)
            {
                // draw a box of points around the target
                renderOffset.Add(new List<int>
                {
                    (int) (RenderTargets[i].Position.X + RenderOffset.X),
                    (int) (RenderTargets[i].Position.X - RenderOffset.X),
                    (int) (RenderTargets[i].Position.Y + RenderOffset.Y),
                    (int) (RenderTargets[i].Position.Y - RenderOffset.Y)
                });
            }

            // Create a list for all possible renderable assets
            IList<string> renderableAssets = new List<string>();

            // Loop through all the provided assets
            foreach (IAsset asset in assets)
            {
                // Loop through all the offsets
                foreach (IList<int> offset in renderOffset)
                {
                    // If the asset is within the offset box then we need to render it
                    if (asset.Position.X < offset[0] && asset.Position.X > offset[1] && asset.Position.Y < offset[2] &&
                        asset.Position.Y > offset[3])
                    {
                        // Add the asset to renderable assets and then break from the offset loop
                        renderableAssets.Add(asset.UniqueName);
                        break;
                    }
                }
            }
            // Return the renderable assets
            return renderableAssets;
        }
    }
}
