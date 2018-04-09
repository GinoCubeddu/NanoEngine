﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Core.Camera
{
    public class Camera2D : ICamera2D
    {
        // Public getter to return the transform value of the camera        
        public Matrix Transform { get; private set; }

        // Holds the asset that the camera is focused on
        private IAsset _focusedAsset;

        private Rectangle _levelBounds;

        // Holds the center point of the viewport
        private static Vector2 _viewportCenter;
        
        public Camera2D(IAsset asset)
        {
            _focusedAsset = asset;
            _levelBounds = Rectangle.Empty;
            Update();
        }

        /// <summary>
        /// Sets the bounds for the level
        /// </summary>
        /// <param name="levelBounds">The bounding box of the level</param>
        public void SetLevelBounds(Rectangle levelBounds)
        {
            _levelBounds = levelBounds;
        }

        /// <summary>
        /// Sets the size of the viewport
        /// </summary>
        /// <param name="viewport">The current size of the viewport</param>
        public static void SetViewport(Viewport viewport)
        {
            _viewportCenter = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        /// <summary>
        /// Updates the camera location based on the assets position
        /// </summary>
        public void Update()
        {
            // Get the X and Y draw position of the camera
            float posX = _focusedAsset.Position.X - _viewportCenter.X;
            float posY = _focusedAsset.Position.Y - _viewportCenter.Y;

            // If the x or y is less than 0 then set them to 0
            if (posX < 0)
                posX = 0;

            if (!_levelBounds.IsEmpty)
            {
                Vector2 viewportDimentions = _viewportCenter * 2;

                if (posX + (viewportDimentions.X) > _levelBounds.Right)
                    posX = _levelBounds.Right - viewportDimentions.X;

                if (posY + (viewportDimentions.Y) > _levelBounds.Bottom)
                    posY = _levelBounds.Bottom - viewportDimentions.Y;
            }

            if (posY < 0)
                posY = 0;

            // Create the new matrix for tha camera position
            Transform = Matrix.CreateTranslation(
                new Vector3(-posX, -posY, 0)
            );
        }

        /// <summary>
        /// Changes the focus of the camera to a new asset
        /// </summary>
        /// <param name="asset">The asset to be focused on</param>
        public void ChangeFocusedAsset(IAsset asset)
        {
            _focusedAsset = asset;
        }
    }
}
