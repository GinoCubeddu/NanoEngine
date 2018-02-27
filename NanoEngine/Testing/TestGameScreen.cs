using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Collision.CollisionTypes;
using NanoEngine.Events.Args;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.Control;
using NanoEngine.StateManagement.States;
using NanoEngine.Testing;
using NanoEngine.Testing.Tiles;
using NanoEngine.Testing.Assets;

namespace NanoEngine
{
    class TestGameScreen : GameScreen, IKeyboardWanted
    {
        private ITileManager tileManager;

        protected override void Draw(IRenderManager renderManager)
        {
            tileManager.DrawTileMap(renderManager);
        }

        public override void LoadContent()
        {
            _assetManager.CreateAsset<Hex, BlankMind>("hex", new Vector2(300, 300));
            _assetManager.CreateAsset<Hex, TestMind>("player", new Vector2(20, 256));
            _assetManager.CreateAsset<ZombieAsset, ZombieMind>("zombie", new Vector2(350, 0));
            _assetManager.CreateAsset<ZombieAsset, ZombieMind>(new Vector2(350, 150));
            _assetManager.CreateAsset<ZombieAsset, ZombieMind>(new Vector2(350, 256));
            tileManager = new TileManager();
            tileManager.AddTile<DirtTile>("dirt_tile");
            tileManager.AddTile<GrassTile>("grass_tile");
            tileManager.LoadTileMap("TestLevel", 7);
            AddCamera("player", _assetManager.RetriveAsset("player"));
            AddCamera("zombie", _assetManager.RetriveAsset("zombie"));
        }

        protected override void Update()
        {

        }

        /// <summary>
        /// Reciver for the keyboard change event
        /// </summary>
        /// <param name="sender">The handler that sent the event</param>
        /// <param name="args">The aguemnts contained within the event</param>
        public void OnKeyboardChange(object sender, NanoKeyboardEventArgs args)
        {
            if (args.TheKeys.ContainsKey(KeyStates.Pressed))
            {
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D1))
                    ChangeCamera("player");
                else if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D2))
                    ChangeCamera("zombie");
            }
        }
    }
}
