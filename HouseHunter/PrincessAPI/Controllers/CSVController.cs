using System.Web;
using System.Web.Http;
using PrincessAPI.Utility.CSVParser;

namespace PrincessAPI.Controllers
{
    [RoutePrefix("api/csv")]
    public class CSVController : ApiController
    {
        [Route("")]
        [HttpPut]
        public void UpdateCSV()
        {
            // Update database
            var path = HttpContext.Current.Server.MapPath("~/");
            path += "Utility/CSVParser/CSV/result2.csv";
            CSVHouseParser.UpdateDatabase(path);
        }
    }
}
