using NanoEngine.Core.Interfaces;
using NanoEngine.Core.Managers;
using NanoEngine.ObjectManagement.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectTypes.General
{
    public abstract class GameScreen : IGameScreen
    {
        /// <summary>
        /// Abstract method to force sub classes to implement it. It is used to load content to the screen 
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Virtural method that sub classes can override to unload content
        /// </summary>
        public virtual void UnloadContent()
        {
            ContentManagerLoad.Manager.UnloadAll();
            AssetFactory.Manager.emptyList();
            AiFactory.Manager.EmptyList();
        }

        /// <summary>
        /// Abstract method to force sub classes to implement it. It is used to update the screen 
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Abstarct method to force sub classes to implement it
        /// </summary>
        /// <param name="renderManager">An instance of the render manager which will be used to draw</param>
        public abstract void Draw(IRenderManager renderManager);
    }
}
