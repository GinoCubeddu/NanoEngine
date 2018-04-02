using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Assets
{
    public class Hex : Entity, ISATColidable
    {
        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Texture2D>("hex"));

            IList<Vector2> points = new List<Vector2>();
            points.Add(new Vector2(-19, -32));
            points.Add(new Vector2(-37, -10));
            points.Add(new Vector2(-37, 10));
            points.Add(new Vector2(-19, 32));
            points.Add(new Vector2(19, 32));
            points.Add(new Vector2(37, 10));
            points.Add(new Vector2(37, -10));
            points.Add(new Vector2(19, -32));

            AddPoints("main", points);
        }
    }
}
