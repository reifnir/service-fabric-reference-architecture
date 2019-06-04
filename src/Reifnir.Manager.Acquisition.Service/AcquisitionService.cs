using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Reifnir.Manager.Acquisition.Interface;

namespace Reifnir.Manager.Acquisition.Service
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed partial class AcquisitionService : StatelessService
    {
        private readonly Uri FabricApplicationBaseUri;
        public AcquisitionService(StatelessServiceContext context)
            : base(context)
        {
            //Many of the Uri helper methods don't help as this the scheme only has a single forward slash
            var serviceAddressComponents = context.ServiceName.AbsoluteUri.Split('/');
            FabricApplicationBaseUri = new Uri($"fabric:/{serviceAddressComponents[1]}/", UriKind.Absolute);
        }

        /// <summary>
        /// Initialize remoting listener
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            //return new ServiceInstanceListener[0]; <-- default implementation

            // this implementation comes from an extension method in the Microsoft.ServiceFabric.Services.Remoting.Runtime namespace
            return this.CreateServiceRemotingInstanceListeners();
        }

        /// <summary>
        /// Any additional processing that must be run during service initialization would go in here. For
        /// Example: subscribe to events, loading reference data into memory, setup an ORM session factory
        /// in a Resource Access component...
        /// 
        /// Note from Microsoft: The runtime waits for OpenAsync to finish before marking the replica as ready
        ///     https://github.com/Azure/service-fabric-issues/issues/1257
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task OnOpenAsync(CancellationToken cancellationToken)
        {
            return base.OnOpenAsync(cancellationToken);
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// 
        /// Warning: Respect all cancellation tokens or you'll have headaches at runtime!
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // If you have logic that must run throughout the lift of your service, put that here!

            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
