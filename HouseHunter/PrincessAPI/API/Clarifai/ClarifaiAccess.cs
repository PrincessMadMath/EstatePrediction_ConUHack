using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrincessAPI.Models;
using PrincessAPI.Utility.Web;

namespace PrincessAPI.API.Clarifai
{
    public class ClarifaiAccess
    {
        private const string ClientId = "1z6E91EP757L7x7PhI5MyGJmyKgL-tBWpCRCW0q5";
        private const string ClientSecret = "N1xX2RDEhfLhmovsoafCS87JCx0EJptJ_iGuOgc4";

        public static string AccessToken { get; set; }

        /// <summary>
        /// Get the Access Token from the Clarifai token api
        /// </summary>
        public static void Authentify()
        {
            if (!string.IsNullOrEmpty(AccessToken))
                return;

            const string url = "https://api.clarifai.com/v1/token/";
            var values = new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", ClientId},
                    {"client_secret", ClientSecret}
                };

            var responseString = HttpRequestHelper.PostAsync(url, values).Result;

            var data = (JObject)JsonConvert.DeserializeObject(responseString);
            if (data["access_token"] != null)
            {
                AccessToken = (string)data["access_token"];
            }
        }

        /// <summary>
        /// Get information on the Clarifai API subscription
        /// </summary>
        public static string GetInfo()
        {
            if (string.IsNullOrEmpty(AccessToken))
                return "";

            const string url = "https://api.clarifai.com/v1/info";
            return HttpRequestHelper.GetAsync(url, AccessToken).Result;
        }

        /// <summary>
        /// Get tag information from a url
        /// </summary>
        /// <param name="url"></param>
        public static string GetTagFromUrl(string url)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return "";

            var endpoint = "https://api.clarifai.com/v1/tag/?url=" + url;
            return HttpRequestHelper.GetAsync(endpoint, AccessToken).Result;
        }

        public static List<string> GetTagsFromJson(string json)
        {
            var data = (JObject)JsonConvert.DeserializeObject(json);
            if (data["results"] != null)
            {
                var classes = new List<string>();

                foreach (JValue classe in data["results"][0]["result"]["tag"]["classes"])
                {
                    classes.Add(classe.Value.ToString());
                }
                return classes;
            }

            return new List<string>();
        }

        public static List<string> GetTagFromUrls(List<Url> urls)
        {
            var taglist = new List<string>();
            foreach (var url in urls)
            {
                var result = GetTagFromUrl(url.url);
                taglist = taglist.Concat(GetTagsFromJson(result)).ToList();
            }

            return taglist.Distinct().ToList();
        }

        public static HousePredictionTags GetPredictionTags(List<string> tags)
        {
            var output = new HousePredictionTags();

            foreach (var tag in tags)
            {
                switch (tag.ToLower())
                {
                    case "":
                        break;
                }
            }

            return output;
        }
    }
}