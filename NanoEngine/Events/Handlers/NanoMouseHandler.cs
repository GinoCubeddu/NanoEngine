using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using NanoEngine.Core.Interfaces;
using NanoEngine.Events.Args;
using NanoEngine.ObjectTypes.Control;

namespace NanoEngine.Events.Handlers
{
    public class NanoMouseHandler : INanoEventHandler
    {
        //Event to handle when the mouse has been clicked
        private event EventHandler<NanoMouseEventArgs> OnMouseChanged;

        //Two states to hold the current and the previous recored states
        private MouseState _currentMouseState, _previousMouseState;

        /// <summary>
        /// Handles the updating for the handler
        /// </summary>
        /// <param name="updateManager">An instance of the update manager</param>
        public void Update(IUpdateManager updateManager)
        {
            // There is no point in checking the input if nothing is subsribed
            if (OnMouseChanged == null)
                return;

            //Set the previous state to the current state
            _previousMouseState = _currentMouseState;
            //Set the current state to the mouse state
            _currentMouseState = Mouse.GetState();

            // If the mouse state has changed then fire an event
            if (_previousMouseState != _currentMouseState)
                MouseStateChanged(_currentMouseState);
        }

        /// <summary>
        /// Fire event for the mouse
        /// </summary>
        /// <param name="currentMouseState"></param>
        private void MouseStateChanged(MouseState currentMouseState)
        {
            OnMouseChanged?.Invoke(this, new NanoMouseEventArgs() {CurrentMouseState = currentMouseState});
        }

        /// <summary>
        /// Subsribes the passed in subscribe to the event handler
        /// </summary>
        /// <param name="subscriber">The subscriber that wants to subsribe</param>
        public void Subscribe(INanoEventSubscribe subscriber)
        {
            // Only subscribe if the subscriber is of the correct type
            if (subscriber is IMouseWanted)
                OnMouseChanged += ((IMouseWanted) subscriber).OnMouseChanged;

        }

        /// <summary>
        /// Desubsribes the passed in subscribe from the event handler
        /// </summary>
        /// <param name="subscriber">The subscriber that wants to desubsribe</param>
        public void Desubscribe(INanoEventSubscribe subscriber)
        {
            // Only desubscribe if the subscriber is of the correct type
            if (subscriber is IMouseWanted)
                OnMouseChanged -= ((IMouseWanted)subscriber).OnMouseChanged;
        }
    }
}
