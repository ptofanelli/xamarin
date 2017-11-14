using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamNativeUtils.Droid.Media;
using XamNativeUtils.Media;
using Android.Media;

[assembly: Dependency(typeof(MediaAndroid))]
namespace XamNativeUtils.Droid.Media
{
    public class MediaAndroid : IMedia
    {
        private MediaPlayer player;

        public void PlayAudio(string FileName)
        {
            if (player != null)
            {
                player.Reset();
                player.Release();
                player = null;
            }

            try
            {
                player = new MediaPlayer();
                var fd = global::Android.App.Application.Context.Assets.OpenFd(FileName);
                player.Prepared += (s, e) =>
                {
                    player.Start();
                };
                player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
                player.Prepare();
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                if (player != null)
                {
                    player.Reset();
                    player.Release();
                    player = null;
                }
            }
        }

        public void Vibrate(int duration)
        {
            var v = (Vibrator)Android.App.Application.Context.GetSystemService(Android.App.Application.VibratorService);
            v.Vibrate(200);
        }

    }
}