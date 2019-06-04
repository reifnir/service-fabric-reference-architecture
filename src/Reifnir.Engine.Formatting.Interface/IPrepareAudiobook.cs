using Microsoft.ServiceFabric.Services.Remoting;
using Reifnir.Engine.Formatting.Interface.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reifnir.Engine.Formatting.Interface
{
    public interface IPrepareAudiobook : IService
    {
        Task<FormatResponse> FormatAsync(string assetId);
    }
}
