using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Tiles
{
    class ChestAsset : Entity
    {
        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Texture2D>("Chest"));
        }
    }
}
