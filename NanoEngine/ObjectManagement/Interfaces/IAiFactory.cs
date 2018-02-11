using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectManagement.Interfaces
{
    public interface IAiFactory
    {
        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <typeparam name="T">Type of AI wanted</typeparam>
        /// <param name="asset"></param>
        /// <returns>The created AI</returns>
        IAiComponent CreateAI<T>(IAsset asset) where T : IAiComponent, new();
    }
}
