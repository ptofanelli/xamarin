using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.ExternalApps.QrDroid
{
    public class BarcodeScannerEventArgs : EventArgs
    {
        public String CodeRead
        {
            get;
            private set;
        }

        public BarcodeScannerEventArgs(String codeRead)
        {
            this.CodeRead = codeRead;
        }
    }
}
