using Microsoft.ServiceFabric.Services.Remoting;
using System;

namespace Reifnir.Manager.Acquisition.Interface
{
    /// <summary>
    /// These events are fired off exclusively by the Acquisition Microservice (aka Manager).
    /// It is the single source for these events.
    /// 
    /// Microservices can subscribe to these events and respond according to their own concerns.
    /// Example with the AssetsAcquired event:
    ///     AcquisitionManager subscribes and kicks off the FormatAssets workflow
    ///     NotificationManager subscribes and if the user who kicked-off the acquisition
    ///         wants to receive notification about this event, they can be notified by whatever
    ///         means they are configured (push notification, SMS, email, etc.)
    ///     LibraryManager would subscribe to AssetAcquisitionFailed and AssetsFormatted events
    /// </summary>
    public interface IEvents
    {
        /// <summary>
        /// This event is fired following the successful completion of the "Acquire Assets" use case.
        /// </summary>
        /// <param name="assetId"></param>
        void AssetsAcquired(string assetId);

        /// <summary>
        /// This event is fired following the successful completion of formatting a set of assets.
        /// </summary>
        /// <param name="assetId"></param>
        void AssetsFormatted(string assetId);
    }
}
