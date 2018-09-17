using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GalleryTimeline
{
    public class PostManager : AbstractAzureEasyTableClient<Post>
    {
        const string URL = "https://registrador.azurewebsites.net/tables/posts/";

        public PostManager() : base(URL) { }
    }
}
