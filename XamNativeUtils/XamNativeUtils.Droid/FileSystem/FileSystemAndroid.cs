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
using XamNativeUtils.FileSystem;
using XamNativeUtils.Droid.FileSystem;

[assembly: Xamarin.Forms.Dependency(typeof(FileSystemAndroid))]
namespace XamNativeUtils.Droid.FileSystem
{
    public class FileSystemAndroid : IFileSystem
    {
        public String GetLocalDownloadPathFromUrl(string url)
        {
            string fileName = Android.Net.Uri.Parse(url).Path.Split('/').Last();
            string downloadDir = Xamarin.Forms.Forms.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            return System.IO.Path.Combine(downloadDir, fileName);
        }
    }
}