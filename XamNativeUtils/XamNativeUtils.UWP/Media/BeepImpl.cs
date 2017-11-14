using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Windows.Phone.Devices.Notification;
using XamNativeUtils.Media;
using XamNativeUtils.UWP.Media;

[assembly: Dependency(typeof(MediaUWP))]
namespace XamNativeUtils.UWP.Media
{
    class MediaUWP : IMedia
    {

        public async void PlayAudio(string FileName)
        {
            try
            {
                StorageFolder Folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
                StorageFile sf = await Folder.GetFileAsync(FileName);
                var PlayMusic = new MediaElement();
                PlayMusic.AudioCategory = Windows.UI.Xaml.Media.AudioCategory.Media;
                PlayMusic.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
                PlayMusic.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void Vibrate(int duration)
        {
            //TODO
            //VibrationDevice testVibrationDevice = VibrationDevice.GetDefault();
            //testVibrationDevice.Vibrate(TimeSpan.FromSeconds(3));
        }
    }
}
