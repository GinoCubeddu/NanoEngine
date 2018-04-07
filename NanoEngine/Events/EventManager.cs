using NanoEngine.Events.Handlers;
using NanoEngine.Events.Interfaces;
using NanoEngine.ObjectTypes.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.Events
{
    public class EventManager : IEventManager
    {
        //List to hold the the handlers
        private IList<INanoEventHandler> _handlers;

        // Static dict to hold all the possible handler types
        private static IDictionary<string, Type> _handlerTypes = new Dictionary<string, Type>();

        /// <summary>
        /// Private constructor so it can only be created inside by the getter
        /// </summary>
        public EventManager()
        {
            // Create the new handlers list
            _handlers = new List<INanoEventHandler>();

            // loop through the possible handlers and add them to the list
            foreach (Type handlerType in _handlerTypes.Values)
                _handlers.Add((INanoEventHandler)Activator.CreateInstance(handlerType));
        }

        /// <summary>
        /// Method that adds a delgate to an event
        /// </summary>
        /// <param name="obj">The delgate object</param>
        public void AddDelegates(object obj)
        {
            // Only pass the object if it is of the correct type
            if (obj is INanoEventSubscribe)
            {
                // Pass the object to each handler so they can decide if they want it
                foreach (INanoEventHandler nanoEventHandler in _handlers)
                    nanoEventHandler.Subscribe((INanoEventSubscribe)obj);
            }
        }

        /// <summary>
        /// Method that removes a delgate to an event
        /// </summary>
        /// <param name="obj">The delgate object</param>
        public void RemoveDelegates(object obj)
        {
            // Only pass the object if it is of the correct type
            if (obj is INanoEventSubscribe)
            {
                foreach (INanoEventHandler nanoEventHandler in _handlers)
                    nanoEventHandler.Desubscribe((INanoEventSubscribe)obj);
            }
        }

        /// <summary>
        /// Adds a new event handler to the event manager. The handler must inherit from INanoEventHandler
        /// NOTE: The new handler will only come into play on new screens and screens that are reloaded
        /// </summary>
        /// <param name="handlerName"></param>
        /// <param name="handlerType"></param>
        public static void AddHandlerType(string handlerName, Type handlerType)
        {
            // Only add a new handler if it is of the correct type
            if (typeof(INanoEventHandler).IsAssignableFrom(handlerType))
            {
                // log a warning if we are about to overwite a handler
                if (_handlerTypes.ContainsKey(handlerName))
                    Console.WriteLine("WARNING: About to overwrite the " + handlerType + " handler within the eventmanager");
                
                // Add the handler and return
                _handlerTypes[handlerName] =  handlerType;
                return;
            }

            // log a message if attempting to add a handler that is not the correct type
            Console.WriteLine("Handler is not of type INanoEventHandler skipping addition");
        }

        /// <summary>
        /// Method to update all the handlers
        /// </summary>
        public void Update(IUpdateManager updateManager)
        {
            // loop through each handler and update it
            foreach (INanoEventHandler nanoEventHandler in _handlers)
                nanoEventHandler.Update(updateManager);
        }
    }
}