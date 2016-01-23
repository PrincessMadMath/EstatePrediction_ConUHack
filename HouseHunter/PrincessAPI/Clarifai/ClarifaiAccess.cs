using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PrincessAPI.Clarifai
{
    public class ClarifaiAccess
    {
        private const string ClientId = "1z6E91EP757L7x7PhI5MyGJmyKgL-tBWpCRCW0q5";
        private const string ClientSecret = "N1xX2RDEhfLhmovsoafCS87JCx0EJptJ_iGuOgc4";

        private string AccessToken { get; set; }

        /// <summary>
        /// Get the Access Token from the Clarifai token api
        /// </summary>
        public async void Authentify()
        {
            if(AccessToken == string.Empty)
                return;

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                   { "grant_type", "client_credentials" },
                   { "client_id", ClientId },
                   {"client_secret", ClientSecret }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://api.clarifai.com/v1/token/", content);

                var responseString = await response.Content.ReadAsStringAsync();

                var data = (JObject)JsonConvert.DeserializeObject(responseString);
                if (data["access_token"] != null)
                {
                    AccessToken = (string)data["access_token"];
                }
            }
        }
    }
}