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
        Task<ReformatAssetResponse> IManageAudiobooks.ReformatAssetsAsync(ReformatAssetRequest request)
        {
            if (request?.AssetId != null)
                return SuccesfulFormatResponse(request.AssetId);
            else
                return FailedFormatResponse();
        }

        private Task<ReformatAssetResponse> FailedFormatResponse()
        {
            var response = new ReformatAssetResponse()
            {
                Message = "Didn't pass an AssetId, so how do you expect this to work?"
            };


            return Task.FromResult(response);
        }

        private Task<ReformatAssetResponse> SuccesfulFormatResponse(string assetId)
        {
            var response = new ReformatAssetResponse()
                  {
                      Success = true,
                      AssetId = assetId,
                      Message = "Huzzah!"
                  };

            //TODO: Publish AssetsFormatted event
            return Task.FromResult(response);
        }
    }
}
