using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.ExternalApps.QrDroid
{
    public interface IScanner
    {
        void ReadCode(int requestCode);
    }
}
