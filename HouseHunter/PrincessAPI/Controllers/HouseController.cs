using System.Collections.Generic;
using System.Web.Http;
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
    }
}
