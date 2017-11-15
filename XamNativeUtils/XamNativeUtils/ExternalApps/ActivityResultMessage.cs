using System;
using System.Collections.Generic;
using System.Text;

namespace XamNativeUtils.ExternalApps
{
    public class ActivityResultMessage
    {
        public const String QRDROID_SCAN_RESULT = "QRDROID_SCAN_RESULT";

        public int RequestCode { get; set; }

        public object ResultCode { get; set; }

        public object Data { get; set; }
    }
}
