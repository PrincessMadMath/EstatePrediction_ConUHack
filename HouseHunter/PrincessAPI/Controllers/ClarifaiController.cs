using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrincessAPI.API.AzureEstatePrediction;
using PrincessAPI.API.Clarifai;
using PrincessAPI.Models;

namespace PrincessAPI.Controllers
{
    [RoutePrefix("api/clarifai")]
    public class ClarifaiController : ApiController
    {
        [Route("info")]
        [HttpGet]
        public string GetToken()
        {
            return ClarifaiAccess.GetInfo();
        }

        [Route("key")]
        [HttpGet]
        public string GetKey()
        {
            return ClarifaiAccess.AccessToken;
        }

        // EX : {"url": "asdasdads"}
        [Route("tag")]
        [HttpPost]
        public List<string> TagsFromUrl([FromBody] Url url)
        {
            if(url == null)
                return null;

            var result = ClarifaiAccess.GetTagFromUrl(url.url);
            return ClarifaiAccess.GetTagsFromJson(result);
        }


        // EX : [{"url": "asdasdads"},{"url": "asdasdads"}]
        [Route("tags")]
        [HttpPost]
        public List<string> TagsFromUrls([FromBody] List<Url> link)
        {
            if (link == null)
                return null;

            return ClarifaiAccess.GetTagFromUrls(link);
        }

        [Route("predict")]
        [HttpPost]
        public Prediction PredictPriceFromTags([FromBody] List<Url> link)
        {
            if (link == null)
                return null;

            var tags = ClarifaiAccess.GetTagFromUrls(link);
            var response = EstatePrediction.PredictWithHouseTags(ClarifaiAccess.GetPredictionTags(tags)).Result;

            var data = (JObject)JsonConvert.DeserializeObject(response);
            if (data["Results"] != null)
            {
                return new Prediction()
                {
                    price = (string)data["Results"]["output1"]["value"]["Values"][0][31],
                    error = (string)data["Results"]["output1"]["value"]["Values"][0][32]
                };
            }

            return null;
        }
    }
}
