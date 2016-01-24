using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PrincessAPI.Utility.Web
{
    public class HttpRequestHelper
    {
        public static async Task<string> PostAsync(string url, Dictionary<string,string> parameters, string bearer = "")
        {
            using (var client = new HttpClient())
            {
                if (bearer != "")
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                var content = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> GetAsync(string url, string bearer = "")
        {
            using (var client = new HttpClient())
            {
                // Add bearer if specified
                if(bearer != "")
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

                var response =
                    await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false);

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}