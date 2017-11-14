using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.FileSystem
{
    public interface IFileSystem
    {
        String GetLocalDownloadPathFromUrl(string Url);
    }
}
