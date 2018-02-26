using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.Testing;
using NanoEngine.Testing.Tiles;
using NanoEngine.Testing.Assets;

namespace NanoEngine
{
    class TestGameScreen : GameScreen
    {
        private ITileManager tileManager;

        protected override void Draw(IRenderManager renderManager)
        {
            tileManager.DrawTileMap(renderManager);
        }

        public override void LoadContent()
        {
            _assetManager.CreateAsset<TestAsset, TestMind>("player", new Vector2(20, 256));
            _assetManager.CreateAsset<ZombieAsset, ZombieMind>(new Vector2(350, 0));
            _assetManager.CreateAsset<ZombieAsset, ZombieMind>(new Vector2(350, 150));
            _assetManager.CreateAsset<ZombieAsset, ZombieMind>(new Vector2(350, 256));
            tileManager = new TileManager();
            tileManager.AddTile<DirtTile>("dirt_tile");
            tileManager.AddTile<GrassTile>("grass_tile");
            tileManager.LoadTileMap("TestLevel");

        }

        protected override void Update()
        {

        }
    }
}
