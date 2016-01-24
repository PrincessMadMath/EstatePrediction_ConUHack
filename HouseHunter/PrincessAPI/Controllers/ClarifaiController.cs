using System.Web.Http;
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

        [Route("tag")]
        [HttpPost]
        public string TagsFromUrl([FromBody] Url link)
        {
            return ClarifaiAccess.GetTagFromUrl(link.url);
        }
    }
}
