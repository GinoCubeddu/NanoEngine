using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoEngine.Core.Interfaces;

namespace NanoEngine.Events.Handlers
{
    public interface INanoEventHandler
    {
        /// <summary>
        /// Handles the updating for the handler
        /// </summary>
        /// <param name="updateManager">An instance of the update manager</param>
        void Update(IUpdateManager updateManager);

        /// <summary>
        /// Subsribes the passed in subscribe to the event handler
        /// </summary>
        /// <param name="subscriber">The subscriber that wants to subsribe</param>
        void Subscribe(INanoEventSubscribe subscriber);

        /// <summary>
        /// Desubsribes the passed in subscribe from the event handler
        /// </summary>
        /// <param name="subscriber">The subscriber that wants to desubsribe</param>
        void Desubscribe(INanoEventSubscribe subscriber);
    }
}
