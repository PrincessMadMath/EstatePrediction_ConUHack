using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PrincessAPI.Clarifai
{
    public class ClarifaiAccess
    {
        private const string ClientId = "1z6E91EP757L7x7PhI5MyGJmyKgL-tBWpCRCW0q5";
        private const string ClientSecret = "N1xX2RDEhfLhmovsoafCS87JCx0EJptJ_iGuOgc4";

        public static string AccessToken { get; set; }

        /// <summary>
        /// Get the Access Token from the Clarifai token api
        /// </summary>
        private static async void AuthentifyAsync()
        {
            if (!string.IsNullOrEmpty(AccessToken))
                return;

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", ClientId},
                    {"client_secret", ClientSecret}
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://api.clarifai.com/v1/token/", content);

                var responseString = await response.Content.ReadAsStringAsync();

                var data = (JObject) JsonConvert.DeserializeObject(responseString);
                if (data["access_token"] != null)
                {
                    AccessToken = (string) data["access_token"];
                }
            }
        }
        public static void Authentify()
        {
            AuthentifyAsync();
        }

        /// <summary>
        /// Get information on the Clarifai API subscription
        /// </summary>
        private static async Task<string> GetInfoAsync()
        {
            // Do nothing if token is empty
            if (string.IsNullOrEmpty(AccessToken))
                return "";

            using (var client = new HttpClient())
            {
                // Add Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var response =
                    await client.GetAsync("https://api.clarifai.com/v1/info", HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false);
                //var response = await client.GetStringAsync("https://api.clarifai.com/v1/info");
                return await response.Content.ReadAsStringAsync();
            }
        }
        public static string GetInfo()
        {
            return GetInfoAsync().Result;
        }

        /// <summary>
        /// Get tag information from a url
        /// </summary>
        /// <param name="url"></param>
        private static async Task<string> GetTagFromUrlAsync(string url)
        {
            // Do nothing if token is empty
            if (string.IsNullOrEmpty(AccessToken))
                return "";

            using (var client = new HttpClient())
            {
                // Add Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var values = new Dictionary<string, string>
                {
                    {"url", url}
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://api.clarifai.com/v1/tag/", content);

                return await response.Content.ReadAsStringAsync();
            }

        }
        public static string GetTagFromUrl(string url)
        {
            return GetTagFromUrlAsync(url).Result;
        }
    }
}