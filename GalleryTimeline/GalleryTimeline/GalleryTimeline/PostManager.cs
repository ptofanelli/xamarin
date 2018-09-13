using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GalleryTimeline
{
    public class PostManager
    {
        const string Url = "https://registrador.azurewebsites.net/tables/posts/";


        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            /*
            headers = headers.set("Content-Type", "application/json");
            headers = headers.set('Access-Control-Allow-Origin', '*');
            headers = headers.set('Access-Control-Allow-Methods', 'POST, GET, OPTIONS, PUT, DELETE');
            */

            return client;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Post>>(result);
        }

        public async Task<Post> Add(string text, string imageBase64)
        {
            Post post = new Post()
            {
                Text = text,
                ImageBase64 = imageBase64
            };

            HttpClient client = GetClient();
            var response = await client.PostAsync(
                Url,
                new StringContent(
                    JsonConvert.SerializeObject(post),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
        }

        public async Task Update(Post post)
        {
            HttpClient client = GetClient();
            string json = JsonConvert.SerializeObject(post);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, Url+post.Id) { Content = content};
            await client.SendAsync(request);
        }

        public async Task Delete(string id)
        {
            HttpClient client = GetClient();
            await client.DeleteAsync(Url + id);
        }
    }
}
