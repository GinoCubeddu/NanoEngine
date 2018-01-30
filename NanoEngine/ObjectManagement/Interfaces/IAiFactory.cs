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
        /// Method to update the behaviours
        /// </summary>
        void Update();

        /// <summary>
        /// Removes an entity from the list based on its ID
        /// </summary>
        /// <param name="id">Unique ID of an entity</param>
        void RemoveAI(int id);

        /// <summary>
        /// Returns the behaviour by the ID passed in
        /// </summary>
        /// <param name="id">Id for the desired entity</param>
        /// <returns>Entity qith the desired id</returns>
        IAIComponent getAI(int id);

        /// <summary>
        /// Emptys the current AI list
        /// </summary>
        void EmptyList();

        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <typeparam name="T">Type of AI wanted</typeparam>
        /// <param name="ent"></param>
        /// <returns>The created AI</returns>
        IAIComponent CreateAI<T>(IEntity ent) where T : IAIComponent, new();
    }
}
