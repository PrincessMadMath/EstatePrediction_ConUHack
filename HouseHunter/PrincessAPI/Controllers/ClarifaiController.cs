using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using PrincessAPI.Clarifai;

namespace PrincessAPI.Controllers
{
    [RoutePrefix("api/clarifai")]
    public class ClarifaiController : ApiController
    {
        [HttpGet]
        [Route("info")]
        public string GetToken()
        {
            return ClarifaiAccess.GetInfo();
        }

    }
}
