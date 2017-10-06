using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectManagement.Managers;
using System;

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
            ContentManagerLoad.Manager.Intinalise(Content);

            SceneManager.Manager.setStartScreen<TestScreen>();

            TileManager.Manager.AddTile<GrassTile>("grass_tile");

            // TODO: Add your initialization logic here
            RenderManager.Init(this);
            Components.Add((IGameComponent)RenderManager.Manager);

            UpdateManager.Init(this);
            Components.Add((IGameComponent)UpdateManager.Manager);
            
            base.Initialize();
        }
    }
}
