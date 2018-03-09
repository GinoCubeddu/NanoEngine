using NanoEngine.Events;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Events.Interfaces;

namespace NanoEngine.ObjectManagement.Managers
{
    public class AiFactory : IAiFactory
    {
        private IEventManager _eventManager;

        public AiFactory(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <typeparam name="T">Type of AI wanted</typeparam>
        /// <returns>The created AI</returns>
        public IAiComponent CreateAi<T>() where T : IAiComponent, new()
        {
            //Create new AI
            IAiComponent ai = new T();
            //Check if its certian things
            _eventManager.AddDelegates(ai);
            //Return AI
            return ai;
        }

        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <param name="aiType">The type of ai we want to create</param>
        /// <returns></returns>
        public IAiComponent CreateAi(Type aiType)
        {
            IAiComponent ai = (IAiComponent) Activator.CreateInstance(aiType);
            _eventManager.AddDelegates(ai);
            return ai;
        }
    }
}
