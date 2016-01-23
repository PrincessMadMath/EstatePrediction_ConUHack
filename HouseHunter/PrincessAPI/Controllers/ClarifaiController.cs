using System.Web.Http;
using PrincessAPI.Clarifai;

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

        [Route("tag")]
        [HttpPost]
        public string TagsFromUrl(string url)
        {
            return ClarifaiAccess.GetTagFromUrl(url);
        }

    }
}
