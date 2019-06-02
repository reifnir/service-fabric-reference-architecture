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
        Task<FormatAssetResponse> IManageAudiobooks.ReformatAssetsAsync(FormatAssetRequest request)
        {
            if (request?.AssetId != null)
                return SuccesfulFormatResponse(request.AssetId);
            else
                return FailedFormatResponse();
        }

        private Task<FormatAssetResponse> FailedFormatResponse()
        {
            var response = new FormatAssetResponse()
            {
                Message = "Didn't pass an AssetId, so how do you expect this to work?"
            };


            return Task.FromResult(response);
        }

        private Task<FormatAssetResponse> SuccesfulFormatResponse(string assetId)
        {
            var response = new FormatAssetResponse()
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
