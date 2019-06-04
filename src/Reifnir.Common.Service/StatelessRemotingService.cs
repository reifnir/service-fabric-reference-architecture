using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Linq;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using System.Fabric;
namespace Reifnir.Common.Service
{
    internal abstract class StatelessRemotingService : StatelessService, IService
    {
        public StatelessRemotingService(StatelessServiceContext serviceContext)
            : base(serviceContext)
        {
            //TODO: Add logic to base StatelessRemotingService constructor that ensure that the service listening address follows the convention
        }

        /// <summary>
        /// To create any service listeners that are required in addition to the standard remoting listening listener, override this method.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<ServiceInstanceListener> CreateAdditionalServiceInstanceListeners()
        {
            return Enumerable.Empty<ServiceInstanceListener>();
        }

        /// <summary>
        /// The standard StatelessService.CreateServiceInstanceListeners method is sealed in order to ensure that a ServiceRemotingInstanceListener is created.
        /// 
        /// If any other listeners are required, such as in the case of subscribing to ServiceBus or RabbitMQ, override: CreateAdditionalServiceInstanceListeners
        /// </summary>
        /// <returns></returns>
        protected sealed override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            var listeners = new List<ServiceInstanceListener>();
            listeners.AddRange(this.CreateServiceRemotingInstanceListeners());
            listeners.AddRange(CreateAdditionalServiceInstanceListeners());
            return listeners.AsReadOnly();
        }
    }
}
