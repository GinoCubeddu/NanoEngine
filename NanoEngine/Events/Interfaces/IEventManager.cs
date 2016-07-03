using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events.Interfaces
{
    public interface IEventManager
    {
                /// <summary>
        /// Method that adds a delgate to an event
        /// </summary>
        /// <param name="obj">The delgate object</param>
        void AddDelegates(object obj);     

        /// <summary>
        /// Method that removes a delgate to an event
        /// </summary>
        /// <param name="obj">The delgate object</param>
        void RemoveDelegates(object obj);

        /// <summary>
        /// Method to update all the handlers
        /// </summary>
        void Update();
    }
}
