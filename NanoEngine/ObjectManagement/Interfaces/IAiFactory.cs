using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Events.Interfaces;

namespace NanoEngine.ObjectManagement.Interfaces
{
    public interface IAiFactory
    {
        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <typeparam name="T">Type of AI wanted</typeparam>
        /// <returns>The created AI</returns>
        IAiComponent CreateAi<T>() where T : IAiComponent, new();

        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <param name="aiType">The type of ai we want to create</param>
        /// <returns></returns>
        IAiComponent CreateAi(Type aiType);
    }
}
