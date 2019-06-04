using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Reifnir.Engine.Formatting.Interface;
using Reifnir.Engine.Formatting.Interface.Model;

namespace Reifnir.Engine.Formatting.Service
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed partial class FormattingService : IPrepareAudiobook
    {
        Task<FormatResponse> IPrepareAudiobook.FormatAsync(string assetId)
        {
            return Task.FromResult(assetId.Contains("this-should-succeed")
                ? new FormatResponse() {  AssetId = assetId, Success = true, Message = "Assets formatted" }
                : new FormatResponse() {  AssetId = assetId, Success = false, Message = "Something went terribly wrong" });
        }
    }
}
