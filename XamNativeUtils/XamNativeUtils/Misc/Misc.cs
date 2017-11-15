using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamNativeUtils.Misc
{
    public class Misc : IMisc
    {
        private IMisc native;

        public Misc()
        {
            native = DependencyService.Get<IMisc>();
            if (native == null)
            {
                throw new NotImplementedException();
            }
        }

        public void CloseApp()
        {
            native.CloseApp();
        }
    }
}
