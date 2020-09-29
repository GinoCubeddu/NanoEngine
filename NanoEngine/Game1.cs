using Microsoft.Xna.Framework;
using NanoEngine.ObjectManagement;
using System;

namespace NanoEngine
{
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

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            RenderFilter.RenderOffset = new Vector2(1500, 200);
            NanoEngineInit.Initialize(GraphicsDevice, this, Content);
            base.Initialize();
        }
    }
}
