using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Events;
using NanoEngine.ObjectManagement.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Locator;

namespace NanoEngine.Core.Managers
{
    public class UpdateManager : GameComponent, IUpdateManager
    {
        //private static field to hold the instace of Game1
        private static Game _game1;

        public GameTime GameTime { get; private set; }

        //private key to hold the pause key
        private Keys pauseButton;

        //public setetr for pause button
        public Keys PauseButton
        {
            set { pauseButton = value; }
        }

        //private filed to hold refrence to the manager
        private static bool Created;

        /// <summary>
        /// Constructor for the update manager
        /// </summary>
        /// <param name="game">The game that the manager will be updating</param>
        public UpdateManager(Game game)
            : base(game)
        {
            if (Created)
                throw new Exception("Only one instance of the UpdateManager may be created");
            _game1 = game;
            Created = true;
        }

        /// <summary>
        /// Main update loop of the program
        /// </summary>
        /// <param name="gameTime">The current time of the game</param>
        public override void Update(GameTime gameTime)
        {
            this.GameTime = gameTime;
            ServiceLocator.Instance.RetriveService<ISceneManager>(DefaultNanoServices.SceneManager).Update(this);
        }

        /// <summary>
        /// Method allows us to change the amount of times the program loops in one second
        /// </summary>
        /// <param name="callsPerSecond">How many times per second</param>
        public void ChangeGameLoopAmount(int callsPerSecond)
        {
            _game1.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1000 / callsPerSecond);
        }

        /// <summary>
        /// Method to exit the game
        /// </summary>
        public void ExitGame()
        {
            // _game1.Exit();
        }

        /// <summary>
        /// Method to load the content
        /// </summary>
        public void LoadContent()
        {

        }

        /// <summary>
        /// Method to initalise the manager
        /// </summary>
        public override void Initialize()
        {

        }
    }
}
