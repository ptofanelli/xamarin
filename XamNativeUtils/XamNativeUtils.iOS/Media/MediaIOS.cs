using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using XamNativeUtils.iOS.Media;
using Xamarin.Forms;
using XamNativeUtils.Media;
using AVFoundation;
using AudioToolbox;

[assembly: Dependency(typeof(MediaIOS))]
namespace XamNativeUtils.iOS.Media
{
    public class MediaIOS : NSObject, IMedia, IAVAudioPlayerDelegate
    {
        AVAudioPlayer _player;

        public void PlayAudio(string fileName)
        {
            try
            {
                NSError error = null;
                AVAudioSession.SharedInstance().SetCategory(AVAudioSession.CategoryPlayback, out error);

                // Any existing sound effect?
                if (_player != null)
                {
                    //Stop and dispose of any sound effect
                    _player.Stop();
                    _player.Dispose();
                }

                var url = NSUrl.FromString(fileName);
                _player = AVAudioPlayer.FromUrl(url);
                _player.Delegate = this;
                _player.Volume = 1.0f;
                _player.PrepareToPlay();
                _player.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
                {
                    Console.WriteLine("FinishedPlaying");
                    _player = null;

                };
                _player.NumberOfLoops = 0;
                _player.Play();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was a problem playing audio: ");
                Console.WriteLine(ex.Message);
            }
        }

        public void Vibrate(int duration)
        {
            //Vibrate
            SystemSound.Vibrate.PlayAlertSound();
        }
    }
}