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
        private IList<IHandler> _handlers;

        /// <summary>
        /// Private constructor so it can only be created inside by the getter
        /// </summary>
        public EventManager()
        {
            //Initialise the instance variables
            _handlers = new List<IHandler>();
            //Add handlers to list
            _handlers.Add(new MouseHandler());
            _handlers.Add(new KeyboardHandler());
        }

        /// <summary>
        /// Method that adds a delgate to an event
        /// </summary>
        /// <param name="obj">The delgate object</param>
        public void AddDelegates(object obj)
        {
            if(obj is IMouseWanted)
            {
                (_handlers[0] as IMouseHandler).GetOnMouseDown += (obj as IMouseWanted).OnMouseDown;
                (_handlers[0] as IMouseHandler).GetOnMouseUp += (obj as IMouseWanted).OnMouseUp;
                (_handlers[0] as IMouseHandler).GetOnMouseMoved += (obj as IMouseWanted).OnMouseMoved;
            }

            if (obj is IKeyboardWanted)
            {
                (_handlers[1] as IKeyboardHandler).GetOnKeyboardChanged += (obj as IKeyboardWanted).OnKeyboardChange;
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
                (_handlers[0] as IMouseHandler).GetOnMouseDown -= (obj as IMouseWanted).OnMouseDown;
                (_handlers[0] as IMouseHandler).GetOnMouseUp -= (obj as IMouseWanted).OnMouseUp;
                (_handlers[0] as IMouseHandler).GetOnMouseMoved -= (obj as IMouseWanted).OnMouseMoved;
            }

            if (obj is IKeyboardWanted)
            {
                (_handlers[1] as IKeyboardHandler).GetOnKeyboardChanged -= (obj as IKeyboardWanted).OnKeyboardChange;
            }
        }

        /// <summary>
        /// Method to update all the handlers
        /// </summary>
        public void Update()
        {
            //Loop through the handlers and update them.
            foreach(IHandler item in _handlers)
            {
                item.Update();
            }
        }
    }
}