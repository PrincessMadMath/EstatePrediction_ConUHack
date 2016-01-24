using System;
using System.Web;
using PrincessAPI.Utility.Web;

namespace PrincessAPI.API.Google
{
    public class GeoCoding
    {
        private const string ApiKey = "AIzaSyDB_RA6nx7wW-lY_SS8D-chLPsC1ROXYYU";

        public static string GetAddressLonLat(string address)
        {
            var data = new Tuple<int, int>(0,0);
            var url = "https://maps.googleapis.com/maps/api/geocode/json?";

            url += "address=" + HttpContext.Current.Server.UrlEncode(address);
            url += "&key=" + ApiKey;

            return HttpRequestHelper.GetAsync(url).Result;
        }
    }
}