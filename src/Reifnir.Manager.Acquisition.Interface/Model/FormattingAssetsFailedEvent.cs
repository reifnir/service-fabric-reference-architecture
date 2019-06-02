using System;
using System.Collections.Generic;
using System.Text;

namespace Reifnir.Manager.Acquisition.Interface.Model
{
    public class FormattingAssetsFailedEvent
    {
        public string AssetId { get; set; }
        public FormattingFailedErrorCode ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
