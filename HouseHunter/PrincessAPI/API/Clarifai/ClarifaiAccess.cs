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
                    case "asking price":
                        output.asking_price = true;
                        break;
                    case "landscape":
                        output.landscape = true;
                        break;
                    case "scenic":
                        output.scenic = true;
                        break;
                    case "travel":
                        output.travel = true;
                        break;
                    case "environment":
                        output.environment = true;
                        break;
                    case "patio":
                        output.patio = true;
                        break;
                    case "parquet":
                        output.parquet = true;
                        break;
                    case "mansion":
                        output.mansion = true;
                        break;
                    case "yard":
                        output.yard = true;
                        break;
                    case "architecture":
                        output.architecture = true;
                        break;
                    case "interior design":
                        output.interior_design = true;
                        break;
                    case "barn":
                        output.barn = true;
                        break;
                    case "comfort":
                        output.comfort = true;
                        break;
                    case "swimming pool":
                        output.swimming_pool = true;
                        break;
                    case "garage":
                        output.garage = true;
                        break;
                    case "suburb":
                        output.suburb = true;
                        break;
                    case "porch":
                        output.porch = true;
                        break;
                    case "flora":
                        output.flora = true;
                        break;
                    case "modern":
                        output.modern = true;
                        break;
                    case "classic":
                        output.classic = true;
                        break;
                    case "gazebo":
                        output.gazebo = true;
                        break;
                    case "villa":
                        output.villa = true;
                        break;
                    case "hotel":
                        output.hotel = true;
                        break;
                    case "old":
                        output.old = true;
                        break;
                    case "fireplace":
                        output.fireplace = true;
                        break;
                    case "minimalist":
                        output.minimalist = true;
                        break;
                    case "luxury":
                        output.luxury = true;
                        break;
                    case "pavement":
                        output.pavement = true;
                        break;
                    case "pool":
                        output.pool = true;
                        break;
                    case "contemporary":
                        output.contemporary = true;
                        break;
                    case "final amount":
                        output.final_amount = true;
                        break;
                }
            }

            return output;
        }
    }
}