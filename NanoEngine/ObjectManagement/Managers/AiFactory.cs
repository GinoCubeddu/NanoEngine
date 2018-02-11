using NanoEngine.Events;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectManagement.Managers
{
    public class AiFactory : IAiFactory
    {
        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <typeparam name="T">Type of AI wanted</typeparam>
        /// <param name="asset"></param>
        /// <returns>The created AI</returns>
        public IAiComponent CreateAI<T>(IAsset asset) where T : IAiComponent, new()
        {
            //Create new AI
            IAiComponent AI = new T();
            //Init the ai
            AI.InitialiseAiComponent(asset);
            //Check if its certian things
            EventManager.Manager.AddDelegates(AI);
            //Add AI to list
            //Return AI
            return AI;
        }
    }
}
