using Microsoft.ServiceFabric.Services.Remoting;
using Reifnir.Manager.Acquisition.Interface.Model;
using System;
using System.Threading.Tasks;

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
        /// <param name="assetId">AssetId of the that were just formatted.</param>
        Task AssetsAcquiredAsync(string assetId);

        /// <summary>
        /// This event is fired following the successful completion of formatting a set of assets.
        /// </summary>
        /// <param name="assetId">AssetId of the that were just formatted.</param>
        Task AssetsFormattedAsync(string assetId);

        /// <summary>
        /// This event is published when there is a failure that occurs during the FormatAssets use case.
        /// </summary>
        /// <param name="e">State related to failed attempt to format assets.</param>
        /// <returns></returns>
        Task FormattingAssetsFailedAsync(FormattingAssetsFailedEvent e);
    }
}
