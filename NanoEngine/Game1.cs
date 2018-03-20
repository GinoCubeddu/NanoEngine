using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectManagement.Managers;
using NanoEngine.ObjectTypes.General;
using System;
using NanoEngine.Core.Camera;
using NanoEngine.Core.Locator;
using NanoEngine.ObjectManagement;
using NanoEngine.Testing.Assets;
using NanoEngine.Testing.Tiles;

namespace NanoEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        static bool created = false;

        public Game1()
        {
            //Checks to see if more than one game1 class is trying to be created
            if (created == false)
            {
                graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                created = true;
            }
            else
            {
                throw new SystemException("Only 1 game class can be created");
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            LevelLoader.AddLevelAsset<DirtTile, TileMind>(1);
            LevelLoader.AddLevelAsset<GrassTile, TileMind>(2);
            LevelLoader.AddLevelAsset<ChestAsset>(3);
            LevelLoader.AddLevelAsset<CoinAsset>(4);
            LevelLoader.AddLevelAsset<TestAsset, TestMind>(5, "player");

            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            RenderFilter.RenderOffset = new Vector2(1500, 200);

            NanoEngineInit.Initialize(GraphicsDevice, this, Content);

            ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager)
                .LoadSound("test", "door-02");

            ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager)
                .LoadSound("soundTrack", "Desert Theme");

            ServiceLocator.Instance.RetriveService<ISceneManager>(DefaultNanoServices.SceneManager)
                .AddScreen<TestGameScreen>("level1");

            base.Initialize();
        }
    }
}
