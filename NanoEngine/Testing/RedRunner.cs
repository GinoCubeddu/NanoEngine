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

namespace NanoEngine.Testing
{
    class RedRunner : Entity
    {
        /// <summary>
        /// Method to initalise the the entity
        /// </summary>
        public override void Initilise()
        {
            SetTexture(ServiceLocator.Instance.RetriveService<NanoContentManager>(DefaultNanoServices.ContentManager)
                .LoadResource<Texture2D>("1x"));
            AssetAnimation = new Animation.Animation(this);

            AssetAnimation.CreateEmptyState("test", 24);
            AssetAnimation.AddFrameToState("test", new Rectangle(61, 40, 66, 72));
            AssetAnimation.AddFrameToState("test", new Rectangle(146, 40, 66, 74));
            AssetAnimation.AddFrameToState("test", new Rectangle(316, 39, 66, 76));
            AssetAnimation.AddFrameToState("test", new Rectangle(401, 38, 66, 77));
            AssetAnimation.AddFrameToState("test", new Rectangle(486, 37, 66, 78));
            AssetAnimation.AddFrameToState("test", new Rectangle(571, 38, 67, 77));
            AssetAnimation.AddFrameToState("test", new Rectangle(657, 39, 82, 76));
            AssetAnimation.AddFrameToState("test", new Rectangle(758, 40, 89, 72));
            AssetAnimation.AddFrameToState("test", new Rectangle(866, 40, 97, 72));
            AssetAnimation.AddFrameToState("test", new Rectangle(124, 140, 100, 74));
            AssetAnimation.AddFrameToState("test", new Rectangle(243, 139, 96, 76));
            AssetAnimation.AddFrameToState("test", new Rectangle(358, 138, 89, 77));
            AssetAnimation.AddFrameToState("test", new Rectangle(466, 137, 79, 78));
            AssetAnimation.AddFrameToState("test", new Rectangle(564, 136, 67, 79));
            AssetAnimation.AddFrameToState("test", new Rectangle(650, 137, 66, 78));
            AssetAnimation.AddFrameToState("test", new Rectangle(735, 138, 66, 76));
            AssetAnimation.AddFrameToState("test", new Rectangle(820, 139, 66, 71));
        }
    }
}
