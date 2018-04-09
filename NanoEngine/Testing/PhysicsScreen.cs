using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Collision.CollisionTypes;
using NanoEngine.Core.Locator;
using NanoEngine.Core.Managers;
using NanoEngine.Events.Args;
using NanoEngine.ObjectManagement;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.States;
using NanoEngine.Testing;
using NanoEngine.Testing.Tiles;
using NanoEngine.Testing.Assets;
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
