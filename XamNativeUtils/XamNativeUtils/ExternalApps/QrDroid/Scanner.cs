using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamNativeUtils.ExternalApps.QrDroid
{
    

    public class Scanner: IScanner
    {
        public const string ANDROID_EXTRA_RESULT = "la.droid.qr.result";

        private IScanner native;

        public Scanner()
        {
            native = DependencyService.Get<IScanner>();
            if (native == null)
            {
                throw new NotImplementedException();
            }
        }

        public void ReadCode(int requestCode)
        {
            native.ReadCode(requestCode);
        }
    }

   
}
