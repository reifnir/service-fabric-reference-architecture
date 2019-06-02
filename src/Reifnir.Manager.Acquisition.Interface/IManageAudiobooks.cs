using Microsoft.ServiceFabric.Services.Remoting;
using Reifnir.Manager.Acquisition.Interface.Model;
using System;

namespace Reifnir.Manager.Acquisition.Interface
{
    public interface IManageAudiobooks : IService
    {
        /// <summary>
        /// This is the RPC-style method for attempting to reformat assets. This is called by a Client application
        /// (Android App or Web Portal) and awaits formatting to be completed.
        /// 
        /// This variation of the "Format Assets" use case is typically done for one of two reasons:
        /// 1: During review, original audio files were reordered and submitted for reformatting.
        /// 2: Formatting files previously failed, but the system has been enhanced to handle the new conditions.
        /// </summary>
        /// <param name="request"></param>
        ReformatAssetResponse RemormatAssetsAsync(ReformatAssetRequest request);
    }
}
