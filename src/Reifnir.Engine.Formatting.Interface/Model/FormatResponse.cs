using System;
using System.Collections.Generic;
using System.Text;

namespace Reifnir.Engine.Formatting.Interface.Model
{
    public class FormatResponse
    {
        public string AssetId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
