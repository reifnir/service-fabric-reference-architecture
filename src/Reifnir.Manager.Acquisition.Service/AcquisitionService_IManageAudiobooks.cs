using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Reifnir.Engine.Formatting.Interface;
using Reifnir.Manager.Acquisition.Interface;
using Reifnir.Manager.Acquisition.Interface.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Reifnir.Manager.Acquisition.Service
{
    internal sealed partial class AcquisitionService : IManageAudiobooks
    {
        async Task<FormatAssetResponse> IManageAudiobooks.FormatAssetsAsync(FormatAssetRequest request)
        {
            if (request?.AssetId == null)
                //return SuccesfulFormatResponse(request.AssetId);
                return AssetIdNotPassedFailure();

            var proxy = CreateComponentProxy<IPrepareAudiobook>();
            var formatResults = await proxy.FormatAsync(request.AssetId);
            Debug.Assert(formatResults != null, "The results that came back from the proxy came back null and this was unexpected.");

            //TODO: Publish event (either AssetsFormattedAsync or FormattingAssetsFailedAsync) when formatting completes
            return new FormatAssetResponse()
            {
                AssetId = request.AssetId,
                Success = formatResults.Success,
                Message = formatResults.Message
            };
        }

        private FormatAssetResponse AssetIdNotPassedFailure()
        {
            var response = new FormatAssetResponse()
            {
                Message = "Didn't pass an AssetId, so how do you expect this to work?"
            };

            //TODO: Publish AssetsFormatted event
            return response;
        }

        private T CreateComponentProxy<T>()
            where T : class, IService
        {
            var facetInterface = typeof(T);
            var facetNamespaceParts = facetInterface.Namespace.Split('.');
            Debug.Assert(facetNamespaceParts.Length == 4, "Facets on Managers, Engines, and Resource Access are expected to be in a format of <Company>.<Concept (i.e. Manager)>.<Volatility>.Interface.");
            var company = facetNamespaceParts[0];
            var concept = facetNamespaceParts[1];
            var volatility = facetNamespaceParts[2];

            //TODO: Provide config for connection string override for application uri root for heterogenous clusters.
            var uri = new Uri(FabricApplicationBaseUri,  $"{company}.{concept}.{volatility}.Service");
            return ServiceProxy.Create<T>(uri);
        }
    }
}
