using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoEngine.Core.Locator
{
    public class ServiceLocator
    {
        // Private member to hold the singleton instance
        private static ServiceLocator _instance;

        // Public getter to return the current instance or create one if one
        // does not exsist
        public static ServiceLocator Instance
        {
            get { return _instance ?? (_instance = new ServiceLocator()); }
        }

        // Private member to hold all the servicies
        private IDictionary<string, IService> _services;

        /// <summary>
        /// Private constructor so only the class can create it
        /// </summary>
        private ServiceLocator()
        {
            _services = new Dictionary<string, IService>();
        }

        /// <summary>
        /// Provide the service locator with a new service, all service names
        /// will be converted to lower case
        /// </summary>
        /// <param name="serviceName">The name of the service</param>
        /// <param name="service">An instance of the service</param>
        public void ProvideService(string serviceName, IService service)
        {
            // Inform the coder that they are about to overwite an exsisting service
            if (_services.ContainsKey(serviceName.ToLower()))
                Console.WriteLine("WARNING: overwriting an already exsisting service");
            _services[serviceName.ToLower()] = service;
        }

        /// <summary>
        /// Retrives a service from the service locator
        /// </summary>
        /// <param name="serviceName">The name of the service</param>
        /// <returns>The service by that ID</returns>
        public T RetriveService<T>(string serviceName) where T : IService
        {
            // If attempting to get a service that does not exsist
            if (!_services.ContainsKey(serviceName.ToLower()))
                throw new KeyNotFoundException(
                    "The service locator does not have a service under the name " +
                    serviceName.ToLower()
                );

            // Return the service if it exsists
            return (T)_services[serviceName.ToLower()];
        }
    }
}
