using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Tiles
{
    class DirtTile : Entity, IAABBColidable
    {
        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public override void Initilise()
        {
            SetTexture(ContentManagerLoad.Manager.LoadResource<Texture2D>("Dirt"));
        }
    }
}
