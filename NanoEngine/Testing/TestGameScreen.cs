using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
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

namespace NanoEngine
{
    class TestGameScreen : GameScreen, IKeyboardWanted
    {
        private ILevelLoader tileManager;

        protected override void Draw(IRenderManager renderManager)
        {
        }

        public override void LoadContent()
        {
            _assetManager.LoadLevel("Level2");
            _assetManager.CreateAsset<ChestAsset>(10, 10);
            IRenderFilter filter = new RenderFilter();
            filter.AddRenderTarget(_assetManager.RetriveAsset("player"));
            _assetManager.SupplyRenderFilter(filter);
            AddCamera("player", _assetManager.RetriveAsset("player"));
            Camera2D.SetLevelBounds(new Rectangle(0, 0, 12800, 2560));
            EventManager.AddDelegates(this);
            ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager).PlaySoundEffect("soundTrack", true);
        }

        protected override void Update(IUpdateManager updateManager)
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

                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D1))
                    ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager).PlayBaseSoundEffect("test");

                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D2))
                    ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager).ChangeSoundEffectVolume(0.1f);
                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.D3))
                    ServiceLocator.Instance.RetriveService<ISoundManager>(DefaultNanoServices.SoundManager).ChangeSoundEffectVolume(-0.1f);

                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Escape))
                {
                    ServiceLocator.Instance.RetriveService<ISceneManager>(DefaultNanoServices.SceneManager).ReloadScreen("level1");
                } else if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Delete))
                {
                    ServiceLocator.Instance.RetriveService<ISceneManager>(DefaultNanoServices.SceneManager).DeleteScreen("level1");
                }
            }
        }
    }
}
