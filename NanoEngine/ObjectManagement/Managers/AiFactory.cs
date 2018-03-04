﻿using NanoEngine.Events;
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
        /// <returns>The created AI</returns>
        public IAiComponent CreateAi<T>() where T : IAiComponent, new()
        {
            //Create new AI
            IAiComponent ai = new T();
            //Check if its certian things
            EventManager.Manager.AddDelegates(ai);
            //Add AI to list
            //Return AI
            return ai;
        }

        public IAiComponent CreateAi(Type aiType)
        {
            IAiComponent ai = (IAiComponent) Activator.CreateInstance(aiType);
            EventManager.Manager.AddDelegates(ai);
            return ai;
        }
    }
}
