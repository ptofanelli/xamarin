using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamNativeUtils.ExternalApps
{
    public class ExternalApps : IExternalApps
    {

        private IExternalApps native;

        public ExternalApps()
        {
            native = DependencyService.Get<IExternalApps>();
            if (native == null)
            {
                throw new NotImplementedException();
            }
        }

        public void OpenDownloads()
        {
            native.OpenDownloads();
        }
    }
}
