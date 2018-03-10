﻿using NanoEngine.ObjectTypes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Collision.CollisionTypes;
using NanoEngine.Core.Managers;
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
        private ILevelLoader tileManager;

        protected override void Draw(IRenderManager renderManager)
        {
        }

        public override void LoadContent()
        {
            _assetManager.LoadLevel("Level2");
            AddCamera("player", _assetManager.RetriveAsset("player"));
            EventManager.AddDelegates(this);
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

                if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Escape))
                {
                    SceneManager.Manager.ReloadScreen("level1");
                } else if (args.TheKeys[KeyStates.Pressed].Contains(Keys.Delete))
                {
                    SceneManager.Manager.DeleteScreen("level1");
                }
            }
        }
    }
}
