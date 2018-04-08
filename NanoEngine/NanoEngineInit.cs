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
using NanoEngine.Events;
using NanoEngine.Events.Handlers;

namespace NanoEngine
{
    public class NanoEngineInit
    {
        public static void Initialize(GraphicsDevice graphicsDevice, Game game, ContentManager content)
        {
            // Inject the default handlers into the EventManager
            EventManager.AddHandlerType(DefaultNanoEventHandlers.KeyboardHandler, typeof(NanoKeyboardHandler));
            EventManager.AddHandlerType(DefaultNanoEventHandlers.MouseHandler, typeof(NanoMouseHandler));

            // Inject the default services for the engine into the serviceLocator
            ServiceLocator.Instance.ProvideService(DefaultNanoServices.SceneManager, new SceneManager());
            ServiceLocator.Instance.ProvideService(DefaultNanoServices.SoundManager, new SoundManager());
            ServiceLocator.Instance.ProvideService(DefaultNanoServices.ContentManager, new NanoContentManager(content));

            // Set the default viewport for the camera
            Camera2D.SetViewport(graphicsDevice.Viewport);

            // Add the rendermanager as a game component
            IRenderManager renderManager = new RenderManager(game);
            game.Components.Add((IGameComponent)renderManager);

            // Add the updatemanager as a game component
            IUpdateManager updateManager = new UpdateManager(game);
            game.Components.Add((IGameComponent)updateManager);
        }
    }
}
