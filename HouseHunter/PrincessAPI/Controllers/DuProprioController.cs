using System.Web.Http;
using PrincessAPI.Utility.Scraping;

namespace PrincessAPI.Controllers
{
    [RoutePrefix("api/duproprio")]
    public class DuProprioController : ApiController
    {
        [Route("scrap")]
        [HttpGet]
        public void ScrapDuProprio()
        {
            DuProprioScrap.GetAllPages();
        }

    }
}
