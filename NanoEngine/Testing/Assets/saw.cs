using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Locator;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.Testing.Assets
{
    class saw : Entity
    {
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<NanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Texture2D>("60"));
            AssetAnimation = new Animation.Animation(this);
            AssetAnimation.CreateEmptyState("1", 60);
            // ROW 0
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 0, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 0, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 0, 114, 114)); // 3
            AssetAnimation.AddFrameToState("1", new Rectangle(342, 0, 114, 114)); //4
            AssetAnimation.AddFrameToState("1", new Rectangle(456, 0, 114, 114)); //5 
            AssetAnimation.AddFrameToState("1", new Rectangle(570, 0, 114, 114)); //6 
            AssetAnimation.AddFrameToState("1", new Rectangle(684, 0, 114, 114));//7
            AssetAnimation.AddFrameToState("1", new Rectangle(798, 0, 114, 114)); //8

            // ROW 1
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 114, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 114, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 114, 114, 114)); // 3
            AssetAnimation.AddFrameToState("1", new Rectangle(342, 114, 114, 114)); //4
            AssetAnimation.AddFrameToState("1", new Rectangle(456, 114, 114, 114)); //5 
            AssetAnimation.AddFrameToState("1", new Rectangle(570, 114, 114, 114)); //6 
            AssetAnimation.AddFrameToState("1", new Rectangle(684, 114, 114, 114));//7
            AssetAnimation.AddFrameToState("1", new Rectangle(798, 114, 114, 114)); //8

            // ROW 2
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 228, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 228, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 228, 114, 114)); // 3
            AssetAnimation.AddFrameToState("1", new Rectangle(342, 228, 114, 114)); //4
            AssetAnimation.AddFrameToState("1", new Rectangle(456, 228, 114, 114)); //5 
            AssetAnimation.AddFrameToState("1", new Rectangle(570, 228, 114, 114)); //6 
            AssetAnimation.AddFrameToState("1", new Rectangle(684, 228, 114, 114));//7
            AssetAnimation.AddFrameToState("1", new Rectangle(798, 228, 114, 114)); //8

            // ROW 3
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 342, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 342, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 342, 114, 114)); // 3
            AssetAnimation.AddFrameToState("1", new Rectangle(342, 342, 114, 114)); //4
            AssetAnimation.AddFrameToState("1", new Rectangle(456, 342, 114, 114)); //5 
            AssetAnimation.AddFrameToState("1", new Rectangle(570, 342, 114, 114)); //6 
            AssetAnimation.AddFrameToState("1", new Rectangle(684, 342, 114, 114));//7
            AssetAnimation.AddFrameToState("1", new Rectangle(798, 342, 114, 114)); //8

            // ROW 4
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 456, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 456, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 456, 114, 114)); // 3
            AssetAnimation.AddFrameToState("1", new Rectangle(342, 456, 114, 114)); //4
            AssetAnimation.AddFrameToState("1", new Rectangle(456, 456, 114, 114)); //5 
            AssetAnimation.AddFrameToState("1", new Rectangle(570, 456, 114, 114)); //6 
            AssetAnimation.AddFrameToState("1", new Rectangle(684, 456, 114, 114));//7
            AssetAnimation.AddFrameToState("1", new Rectangle(798, 456, 114, 114)); //8

            // ROW 5
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 570, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 570, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 570, 114, 114)); // 3
            AssetAnimation.AddFrameToState("1", new Rectangle(342, 570, 114, 114)); //4
            AssetAnimation.AddFrameToState("1", new Rectangle(456, 570, 114, 114)); //5 
            AssetAnimation.AddFrameToState("1", new Rectangle(570, 570, 114, 114)); //6 
            AssetAnimation.AddFrameToState("1", new Rectangle(684, 570, 114, 114));//7
            AssetAnimation.AddFrameToState("1", new Rectangle(798, 570, 114, 114)); //8

            //ROW 6
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 684, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 684, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 684, 114, 114)); // 3
            AssetAnimation.AddFrameToState("1", new Rectangle(342, 684, 114, 114)); //4
            AssetAnimation.AddFrameToState("1", new Rectangle(456, 684, 114, 114)); //5 
            AssetAnimation.AddFrameToState("1", new Rectangle(570, 684, 114, 114)); //6 
            AssetAnimation.AddFrameToState("1", new Rectangle(684, 684, 114, 114));//7
            AssetAnimation.AddFrameToState("1", new Rectangle(798, 684, 114, 114)); //8

            // ROW 7
            AssetAnimation.AddFrameToState("1", new Rectangle(0, 798, 114, 114)); //1 
            AssetAnimation.AddFrameToState("1", new Rectangle(114, 798, 114, 114)); // 2
            AssetAnimation.AddFrameToState("1", new Rectangle(228, 798, 114, 114)); // 3
        }
    }
}
