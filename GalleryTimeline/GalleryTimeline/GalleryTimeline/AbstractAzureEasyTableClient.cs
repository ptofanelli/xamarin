using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GalleryTimeline
{
    public class AbstractAzureEasyTableClient<T> : IAzureEasyTableClient<T>
    {
        protected string url;
        protected HttpClient client;

        public AbstractAzureEasyTableClient(string url)
        {
            this.url = url;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
        }

        public async Task<T> AddAsync(T obj)
        {
            var response = await client.PostAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(obj),
                    Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            return default(T);
        }

        public async Task<T> DeleteAsync(object id)
        {
            var response = await client.DeleteAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            return default(T);
        }

        public async Task<T> GetAsync(object id)
        {
            string result = await client.GetStringAsync(url+id);
            try
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            string result = await client.GetStringAsync(url);
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<T> UpdateAsync(object id, T obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, url + id) { Content = content };
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            return default(T);
        }
    }
}
