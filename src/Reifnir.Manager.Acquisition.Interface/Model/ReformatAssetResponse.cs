using System;
using System.Collections.Generic;
using System.Text;

namespace Reifnir.Manager.Acquisition.Interface.Model
{
    public class ReformatAssetResponse
    {
        public string AssetId { get; set; }        
        public bool Success { get; set; }
        public string Message { get; set; }
    }

}
