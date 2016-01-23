using System.Web;
using Microsoft.Web.WebSockets;

namespace PrincessAPI.Websocket
{
    /// <summary>
    /// Summary description for WsHttpHandler
    /// </summary>
    public class WsHttpHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(new ChatWebsocket());

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

