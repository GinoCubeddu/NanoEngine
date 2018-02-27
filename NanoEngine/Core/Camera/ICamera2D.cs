using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Core.Camera
{
    public interface ICamera2D
    {
        // Public getter to return the transform value of the camera        
        Matrix Transform { get; }

        /// <summary>
        /// Updates the camera location based on the assets position
        /// </summary>
        void Update();

        /// <summary>
        /// Changes the focus of the camera to a new asset
        /// </summary>
        /// <param name="asset">The asset to be focused on</param>
        void ChangeFocusedAsset(IAsset asset);
    }
}
