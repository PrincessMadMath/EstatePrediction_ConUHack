using PrincessAPI.Utility.Web;

namespace PrincessAPI.Utility.Scraping
{
    public class DuProprioScrap
    {
        public static void GetAllPages()
        {
            var url = "http://duproprio.com/search/?hash=/s-filter=featured/s-hide-sold=/s-mode=list/p-con=main/p-ord=date/p-dir=DESC/pa-ge=1";

            var data = HttpRequestHelper.GetAsync(url).Result;
        }
    }
}