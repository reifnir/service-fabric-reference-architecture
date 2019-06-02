using Reifnir.Manager.Acquisition.Interface;
using Reifnir.Manager.Acquisition.Interface.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reifnir.Manager.Acquisition.Service
{
    internal sealed partial class AcquisitionService : IManageAudiobooks
    {
        Task<ReformatAssetResponse> IManageAudiobooks.RemormatAssetsAsync(ReformatAssetRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
