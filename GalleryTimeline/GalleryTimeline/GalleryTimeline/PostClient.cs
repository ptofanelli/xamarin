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
    public class PostsClient : AbstractAzureEasyTableClient<Post>
    {
        const string URL = "https://registrador.azurewebsites.net/tables/posts/";

        public PostsClient() : base(URL) { }
    }
}
