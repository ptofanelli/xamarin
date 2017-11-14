using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamNativeUtils.FileSystem
{
    public class FileSystem : IFileSystem
    {

        private IFileSystem native;

        public FileSystem()
        {
            native = DependencyService.Get<IFileSystem>();
            if (native == null)
            {
                throw new NotImplementedException();
            }
        }

        public string GetLocalDownloadPathFromUrl(string url)
        {
            return native.GetLocalDownloadPathFromUrl(url);
        }
    }
}
