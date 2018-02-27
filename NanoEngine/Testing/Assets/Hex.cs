using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Assets
{
    public class Hex : Entity
    {
        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public override void Initilise()
        {
            SetTexture(ContentManagerLoad.Manager.GetTexture("hex"));
            AddPoint(new Vector2(-37, -10));
            AddPoint(new Vector2(-37, 10));
            AddPoint(new Vector2(-19, 32));
            AddPoint(new Vector2(19, 32));
            AddPoint(new Vector2(37, 10));
            AddPoint(new Vector2(37, -10));
            AddPoint(new Vector2(19, -32));
            AddPoint(new Vector2(-19, -32));
        }
    }
}
