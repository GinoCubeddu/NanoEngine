using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NanoEngine.Core.Camera;
using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Locator;
using NanoEngine.Core.Managers;

namespace NanoEngine
{
    public class NanoEngineInit
    {
        public static void Initialize(GraphicsDevice graphicsDevice, Game game, ContentManager content)
        {
            game.IsMouseVisible = true;
            ServiceLocator.Instance.ProvideService(DefaultNanoServices.SceneManager, new SceneManager());
            ServiceLocator.Instance.ProvideService(DefaultNanoServices.SoundManager, new SoundManager());
            ServiceLocator.Instance.ProvideService(DefaultNanoServices.ContentManager, new NanoContentManager(content));

            Camera2D.SetViewport(graphicsDevice.Viewport);

            // TODO: Add your initialization logic here
            IRenderManager renderManager = new RenderManager(game);
            game.Components.Add((IGameComponent)renderManager);

            IUpdateManager updateManager = new UpdateManager(game);
            game.Components.Add((IGameComponent)updateManager);
        }
    }
}
