using NanoEngine.Events.Handlers;
using NanoEngine.Events.Interfaces;
using NanoEngine.ObjectTypes.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.Events
{
    public class EventManager : IEventManager
    {
        //List to hold the the handlers
        private static List<IHandler> handlers;

        //Filed to hold instance to the handler
        private static IEventManager manager;

        /// <summary>
        /// Getter to return the manager 
        /// </summary>
        public static IEventManager Manager
        {
            get { return manager ?? (manager = new EventManager()); }
        }

        /// <summary>
        /// Private constructor so it can only be created inside by the getter
        /// </summary>
        private EventManager()
        {
            //Initialise the instance variables
            handlers = new List<IHandler>();
            //Add handlers to list
            handlers.Add(new MouseHandler());
            handlers.Add(new KeyboardHandler());
        }

        /// <summary>
        /// Method that adds a delgate to an event
        /// </summary>
        /// <param name="obj">The delgate object</param>
        public void AddDelegates(object obj)
        {
            if(obj is IMouseWanted)
            {
                (handlers[0] as IMouseHandler).GetOnMouseDown += (obj as IMouseWanted).OnMouseDown;
                (handlers[0] as IMouseHandler).GetOnMouseUp += (obj as IMouseWanted).OnMouseUp;
                (handlers[0] as IMouseHandler).GetOnMouseMoved += (obj as IMouseWanted).OnMouseMoved;
            }

            if (obj is IKeyboardWanted)
            {
                (handlers[1] as IKeyboardHandler).GetOnKeyboardChanged += (obj as IKeyboardWanted).OnKeyboardChange;
            }
        }

        /// <summary>
        /// Method that removes a delgate to an event
        /// </summary>
        /// <param name="obj">The delgate object</param>
        public void RemoveDelegates(object obj)
        {
            if (obj is IMouseWanted)
            {
                (handlers[0] as IMouseHandler).GetOnMouseDown -= (obj as IMouseWanted).OnMouseDown;
                (handlers[0] as IMouseHandler).GetOnMouseUp -= (obj as IMouseWanted).OnMouseUp;
                (handlers[0] as IMouseHandler).GetOnMouseMoved -= (obj as IMouseWanted).OnMouseMoved;
            }

            if (obj is IKeyboardWanted)
            {
                (handlers[1] as IKeyboardHandler).GetOnKeyboardChanged -= (obj as IKeyboardWanted).OnKeyboardChange;
            }
        }

        /// <summary>
        /// Method to update all the handlers
        /// </summary>
        public void Update()
        {
            //Loop through the handlers and update them.
            foreach(IHandler item in handlers)
            {
                item.Update();
            }
        }
    }
}