using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.ObjectManagement
{
    public interface IRenderFilter
    {
        /// <summary>
        /// Takes a dictonary of assets and filters out the ones that are not set in the render offset of
        /// the render targets. If no render target
        /// </summary>
        /// <param name="assets">The dictonary of assets to filter</param>
        /// <returns>A dictonary containing only the assets that are in the render zone(s)</returns>
        IDictionary<string, IAsset> SortAssetsInRenderZone(IDictionary<string, IAsset> assets);

        /// <summary>
        /// Takes a dictonary of ai components and filters out the ones that are not set in the render offset of
        /// the render targets. If no render target
        /// </summary>
        /// <param name="aiComponents">The dictonary of ai components to filter</param>
        /// <returns>A dictonary containing only the assets that are in the render zone(s)</returns>
        IDictionary<string, IAiComponent> SortAiInRenderZone(IDictionary<string, IAiComponent> aiComponents);

        /// <summary>
        /// Adds a render target to the RenderFilter
        /// </summary>
        /// <param name="asset">A render target for the filter to use</param>
        void AddRenderTarget(IAsset asset);
    }
}
