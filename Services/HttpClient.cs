using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;

namespace Services
{
    public class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _htppClient;

        public HttpClient(System.Net.Http.HttpClient httpClient)
        {
            _htppClient = httpClient;
        }
        public async  Task<T> GetAsync<T>(Uri url)
        {
            var response = await _htppClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var strRes =  await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(strRes);
        }

        public async Task<T> PostAsync<T>(Uri url, object body)
        {
            var response = await _htppClient.PostAsync(url,new StringContent(JsonSerializer.Serialize(body),Encoding.UTF8,"application/json"));
            response.EnsureSuccessStatusCode();
            var strRes = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(strRes);
        }
    }
}
