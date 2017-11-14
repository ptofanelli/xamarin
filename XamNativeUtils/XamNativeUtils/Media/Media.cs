using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamNativeUtils.Media
{
    public class Media : IMedia
    {
        public const int VIDRATION_DURATION_SHORT = 200;
        public const int VIDRATION_DURATION_LONG = 600;

        private IMedia native;

        public Media()
        {
            native = DependencyService.Get<IMedia>();
            if (native == null)
            {
                throw new NotImplementedException();
            }
        }

        public void PlayAudio(string fileName)
        {
            native.PlayAudio(fileName);
        }

        public void Vibrate(int duration)
        {
            native.Vibrate(duration);
        }
    }
}
