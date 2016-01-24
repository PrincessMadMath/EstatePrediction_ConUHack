using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrincessAPI.API.AzureEstatePrediction;
using PrincessAPI.Models;
using PrincessAPI.Utility.HouseModelQuery;

namespace PrincessAPI.Controllers
{
    [RoutePrefix("api/house")]
    public class HouseController : ApiController
    {
        [Route("localization")]
        [HttpPost]
        public List<HouseModel> GetHousesInArea([FromBody] HouseLocalizationRequest request)
        {
            return HouseModelQuery.GetHouseNear(request);
        }

        [Route("all")]
        [HttpGet]
        public List<HouseModel> GetAllHouses()
        {
            return HouseModelQuery.GetAllHouses();
        }

        [Route("add")]
        [HttpPut]
        public void AddHouseModel([FromBody] HouseModel houseModel)
        {
            HouseModelQuery.InsertHouseModel(houseModel);
        }

        [Route("predicthouse")]
        [HttpPost]
        public string PredictHousePrice([FromBody] FormHouseModel model)
        {
            if(model == null)
                model = new FormHouseModel();

            var result = EstatePrediction.PredictWithHouseStats(model.ToPredictionModel()).Result;

            var data = (JObject)JsonConvert.DeserializeObject(result);
            if (data["Results"] != null)
            {
                return (string)data["Results"]["output1"]["value"]["Values"][0][17];
            }

            return null;
        }

        [Route("predictcondo")]
        [HttpPost]
        public Prediction PredictCondoPrice([FromBody] FormCondoModel model)
        {
            if (model == null)
                model = new FormCondoModel();

            var result = EstatePrediction.PredictWithHouseStats(model.ToPredictionModel()).Result;

            var data = (JObject)JsonConvert.DeserializeObject(result);
            if (data["Results"] != null)
            {
                return new Prediction()
                {
                    price = (string) data["Results"]["output1"]["value"]["Values"][0][17],
                    error = (string) data["Results"]["output1"]["value"]["Values"][0][18]
                };
            }

            return null;
        }

        [Route("predict/tags")]
        [HttpPost]
        public Prediction PredictWithTags([FromBody] HousePredictionTags model)
        {
            if(model == null)
                model = new HousePredictionTags();

            var result = EstatePrediction.PredictWithHouseTags(model).Result;

            var data = (JObject)JsonConvert.DeserializeObject(result);
            if (data["Results"] != null)
            {
                return new Prediction()
                {
                    price = (string) data["Results"]["output1"]["value"]["Values"][0][31],
                    error = (string) data["Results"]["output1"]["value"]["Values"][0][32]
                };
            }

            return null;
        }
    }
}
