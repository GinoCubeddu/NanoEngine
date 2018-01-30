using NanoEngine.Events;
using NanoEngine.ObjectManagement.Interfaces;
using NanoEngine.ObjectTypes.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.ObjectManagement.Managers
{
    public class AIManager : IAiFactory
    {
        //Static field to hold an instance to the manager
        private static IAiFactory manager;

        ///List to hold all the current AI's
        private List<IAIComponent> _AIs;

        ///getter for the instance of the manager
        public static IAiFactory Manager
        {
            get { return manager ?? (manager = new AIManager()); }
        }

        /// <summary>
        /// Private constructor for the AIManager
        /// </summary>
        private AIManager()
        {
            _AIs = new List<IAIComponent>();
        }

        /// <summary>
        /// Method to update all behaviours
        /// </summary>
        public void Update()
        {
            foreach (IAIComponent item in _AIs)
            {
                item.Update();
            }
        }

        /// <summary>
        /// Factory method to create an AI
        /// </summary>
        /// <typeparam name="T">Type of AI wanted</typeparam>
        /// <param name="ent"></param>
        /// <returns>The created AI</returns>
        public IAIComponent CreateAI<T>(IEntity ent) where T : IAIComponent, new()
        {
            //Create new AI
            IAIComponent AI = new T();
            //Init the ai
            AI.Initialise(ent);
            //Check if its certian things
            EventManager.Manager.AddDelegates(AI);
            //Add AI to list
            _AIs.Add(AI);
            //Return AI
            return AI;
        }

        /// <summary>
        /// Emptys the current AI list
        /// </summary>
        public void EmptyList()
        {
            _AIs.Clear();
            _AIs.TrimExcess();
        }

        /// <summary>
        /// Returns the behaviour by the ID passed in
        /// </summary>
        /// <param name="id">Id for the desired entity</param>
        /// <returns>Entity qith the desired id</returns>
        public IAIComponent getAI(int id)
        {
            //Checks if there is an entity in the list that has the passed in ID
            var AI = _AIs.Where(x => x.UID == id).ToArray();
            return AI[0];
        }

        /// <summary>
        /// Removes an entity from the list based on its ID
        /// </summary>
        /// <param name="id">Unique ID of an entity</param>
        public void RemoveAI(int id)
        {
            //Checks if there is an entity in the list that has the passed in ID
            var AI = _AIs.Where(x => x.UID == id).ToArray();
            //Remove entity
            _AIs.Remove(AI[0]);
            EventManager.Manager.RemoveDelegates(AI[0]);
            //Set the entity to null
            AI[0] = null;
        }
    }
}
