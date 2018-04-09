using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Collision.CollidableTypes;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Assets
{
    class plane : Entity, ISATColidable
    {
        public int CollidableId => 8;

        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager).LoadResource<Texture2D>("plane"));
            //IList<Vector2> nozzle = new List<Vector2>();
            //nozzle.Add(new Vector2(2, -105));
            //nozzle.Add(new Vector2(-7, -98));
            //nozzle.Add(new Vector2(7, -98));
            //AddPoints(nozzle);

            IList<Vector2> prepella = new List<Vector2>();
            prepella.Add(new Vector2(-23, -94));
            prepella.Add(new Vector2(21, -94));
            prepella.Add(new Vector2(21, -98));
            prepella.Add(new Vector2(-23, -98));
            AddPoints("prepella", prepella);

            IList<Vector2> frontBody = new List<Vector2>();
            frontBody.Add(new Vector2(-18, -67));
            frontBody.Add(new Vector2(16, -67));
            frontBody.Add(new Vector2(10, -94));
            frontBody.Add(new Vector2(-12, -94));
            AddPoints("frontBody", frontBody);

            IList <Vector2> leftWing = new List<Vector2>();
            
            leftWing.Add(new Vector2(-19, -67)); // right top
            leftWing.Add(new Vector2(-121, -57)); // left top
            leftWing.Add(new Vector2(-121, -34)); // left bot
            leftWing.Add(new Vector2(-19, -28)); // right bot
            AddPoints("leftWing", leftWing);

            IList <Vector2> rightWing = new List<Vector2>();
            rightWing.Add(new Vector2(17, -28)); // left bot
            rightWing.Add(new Vector2(119, -34)); // right bot
            rightWing.Add(new Vector2(119, -57)); // right top
            rightWing.Add(new Vector2(17, -67)); // left top
            AddPoints("rightWing", rightWing);

            IList <Vector2> centerBody = new List<Vector2>();
            centerBody.Add(new Vector2(-19, -28)); 
            centerBody.Add(new Vector2(17, -28));
            centerBody.Add(new Vector2(16, -67));
            centerBody.Add(new Vector2(-18, -67));
            AddPoints("centerBody", centerBody);

            IList <Vector2> backBody = new List<Vector2>();
            backBody.Add(new Vector2(-19, -28));
            backBody.Add(new Vector2(-13, 61));
            backBody.Add(new Vector2(11, 61));
            backBody.Add(new Vector2(17, -28));
            AddPoints("backBody", backBody);

            IList <Vector2> tail = new List<Vector2>();
            tail.Add(new Vector2(-43, 88)); //
            tail.Add(new Vector2(41, 88));
            tail.Add(new Vector2(41, 79));
            tail.Add(new Vector2(13, 61));
            tail.Add(new Vector2(-13, 61));
            tail.Add(new Vector2(-43, 79));
            AddPoints("tail", tail);

            //IList<Vector2> fin = new List<Vector2>();
            //fin.Add(new Vector2(-10, 88));
            //fin.Add(new Vector2(-1, 103));
            //fin.Add(new Vector2(8, 88));
            //AddPoints(fin);
        }
    }
}
