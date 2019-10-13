using Microsoft.Xna.Framework;
using NanoEngine.Core.Interfaces;
using System;
using NanoEngine.Core.Locator;
using NanoEngine.ObjectManagement;
using NanoEngine.ObjectTypes.Assets;
using NanoEngine.Collision;

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


            RenderFilter.RenderOffset = new Vector2(1500, 200);
            Entity.DrawAssetBounds = true;

            NanoEngineInit.Initialize(GraphicsDevice, this, Content);

            // Get the scenemanager from the service locator
            ISceneManager sceneManager = ServiceLocator.Instance.RetriveService<ISceneManager>(DefaultNanoServices.SceneManager);
            ISoundManager soundManager = ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager);

            QuadTree.DrawQuadTrees = true;
            base.Initialize();
        }
    }
}
