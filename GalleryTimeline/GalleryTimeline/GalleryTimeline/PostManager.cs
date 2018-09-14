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

        public async Task<Post> Update(Post post)
        {
            HttpClient client = GetClient();
            string json = JsonConvert.SerializeObject(post);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, Url+post.Id) { Content = content};
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
            }
            return null;

            /*
            using (HttpClientHandler ClientHandler = new HttpClientHandler())
            using (HttpClient Client = new HttpClient(ClientHandler))
            {
                Client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                using (HttpRequestMessage RequestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Url + post.Id))
                {
                    string json = JsonConvert.SerializeObject(post);
                    RequestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (HttpResponseMessage ResponseMessage = await Client.SendAsync(RequestMessage))
                    {
                        Debug.WriteLine("ResponseMessage: " + ResponseMessage);
                        string result = await ResponseMessage.Content.ReadAsStringAsync();
                        Debug.WriteLine("ResponseMessage: " + result);
                    }
                }
            }
            */
        }

        public async Task Delete(string id)
        {
            HttpClient client = GetClient();
            await client.DeleteAsync(Url + id);
        }
    }
}
