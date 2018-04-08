using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.ObjectTypes.General;
using NanoEngine.Testing.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoEngine.Testing
{
    class PhysicsScreen : GameScreen
    {
        public override void LoadContent()
        {
            _assetManager.CreateAsset<BallTwolAsset, BallTwoMind>(300, 200);
            _assetManager.CreateAsset<BallAsset, BallMind>(500, 150);
            _assetManager.CreateAsset<Wall, WallMind>(-64, 0);
            _assetManager.CreateAsset<Wall, WallMind>(800, 0);
            _assetManager.CreateAsset<Wall, WallMind>("top", 0, -64);
            _assetManager.CreateAsset<Wall, WallMind>("bot", 0, 480);
            _assetManager.RetriveAsset("top").SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager).LoadResource<Texture2D>("wall-horizontal"));
            _assetManager.RetriveAsset("bot").SetTexture(ServiceLocator.Instance.RetriveService<INanoContentManager>(DefaultNanoServices.ContentManager).LoadResource<Texture2D>("wall-horizontal"));
        }

        protected override void Draw(IRenderManager renderManager)
        {
        }

        protected override void Update(IUpdateManager updateManager)
        {
        }
    }
}
