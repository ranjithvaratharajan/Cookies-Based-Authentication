using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure
{
    public static class ServiceCall<T>
    {
        const string HTTP_BASE = "http://localhost:65406/";
        public static async Task<HttpResponseMessage> getData(string Url)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(HTTP_BASE);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(Url);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> postData(string Url, T data)
        {
            HttpResponseMessage response = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(HTTP_BASE);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync(Url, content);
            }
            return response;
        }
    }
}
