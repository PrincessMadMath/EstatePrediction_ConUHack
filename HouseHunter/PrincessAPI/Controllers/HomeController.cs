using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace PrincessAPI.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            // get root folder path
            var path = HttpContext.Current.Server.MapPath("~/");
            
            // get the text in the html file
            var text = File.ReadAllText($"{path}/View/index.html");
            var response = new HttpResponseMessage
            {
                Content = new StringContent(text)
            };

            // serve the HTML
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
