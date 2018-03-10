using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectManagement.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Camera;
using NanoEngine.Events;
using NanoEngine.Events.Interfaces;
using NanoEngine.ObjectTypes.Assets;

namespace NanoEngine.ObjectTypes.General
{
    public abstract class GameScreen : IGameScreen
    {
        protected IAssetManager _assetManager;

        private IDictionary<string, ICamera2D> _cameras;

        public ICamera2D Camera2D { get; private set; }

        public double LevelTimer { get; private set; }

        protected IEventManager EventManager;

        /// <summary>
        /// Adds a camera to the level
        /// </summary>
        /// <param name="id">The id of the camera</param>
        /// <param name="asset">The asset the camera should focus on</param>
        protected void AddCamera(string id, IAsset asset)
        {
            // If this is the first camera then create the dict
            if (_cameras == null)
                _cameras = new Dictionary<string, ICamera2D>();

            // Add the camera if one by that ID does not exsist
            if (!_cameras.Keys.Contains(id))
            {
                _cameras.Add(id, new Camera2D(asset));
                // If the current camera is null then set this camera to
                // the main one
                if (Camera2D == null)
                    Camera2D = _cameras[id];
            }
        }

        /// <summary>
        /// Changes the current camera to a different one
        /// </summary>
        /// <param name="id">The id of the desired camera</param>
        protected void ChangeCamera(string id)
        {
            // Attempt to change to the camera otherwise throw an error
            try
            {
                Camera2D = _cameras[id];
            }
            catch (KeyNotFoundException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        /// <summary>
        /// Abstract method to force sub classes to implement it. It is used to load content to the screen 
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Virtural method that sub classes can override to unload content
        /// </summary>
        public virtual void UnloadContent()
        {
            EventManager = null;
            _assetManager = null;
            _cameras = null;
            Camera2D = null;
        }

        /// <summary>
        /// Abstract method to force sub classes to implement it. It is used to update the screen 
        /// </summary>
        protected abstract void Update();

        /// <summary>
        /// Abstarct method to force sub classes to implement it
        /// </summary>
        /// <param name="renderManager">An instance of the render manager which will be used to draw</param>
        protected abstract void Draw(IRenderManager renderManager);

        /// <summary>
        /// Method to Draw the scene
        /// </summary>
        /// <param name="renderManager">RenderManager containing draw methods</param>
        public void DrawScreen(IRenderManager renderManager)
        {
            _assetManager.DrawAssets(renderManager);
            Draw(renderManager);
        }
        /// <summary>
        /// Method to update the GameScreen
        /// </summary>
        /// <param name="updateManager">Provides a refrence to the updateManager.</param>
        public void UpdateScreen(IUpdateManager updateManager)
        {
            LevelTimer += updateManager.gameTime.ElapsedGameTime.TotalSeconds;
            EventManager.Update();
            _assetManager.UpdateAssets();
            // If we have a camera then update it
            if (Camera2D != null)
                Camera2D.Update();
            Update();
        }

        /// <summary>
        /// Initialise's the screen
        /// </summary>
        public void Initialise()
        {
            EventManager = new EventManager();
            _assetManager = new AssetManager(EventManager);
        }
    }
}
