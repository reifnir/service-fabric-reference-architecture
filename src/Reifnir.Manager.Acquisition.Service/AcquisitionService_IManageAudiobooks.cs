using Reifnir.Manager.Acquisition.Interface;
using Reifnir.Manager.Acquisition.Interface.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reifnir.Manager.Acquisition.Service
{
    internal sealed partial class AcquisitionService : IManageAudiobooks
    {
        ReformatAssetResponse IManageAudiobooks.RemormatAssetsAsync(ReformatAssetRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
