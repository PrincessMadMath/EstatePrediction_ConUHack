using Microsoft.Web.WebSockets;

namespace PrincessAPI.Websocket
{
    public class ChatWebsocket : WebSocketHandler
    {

        private static readonly WebSocketCollection Clients = new WebSocketCollection();

        private string _name;

        public override void OnOpen()
        {
            _name = WebSocketContext.QueryString["name"];
            Clients.Add(this);
            Clients.Broadcast(_name + " has connected.");
        }

        public override void OnMessage(string message)
        {
            Clients.Broadcast($"{_name} said: {message}");
        }

        public override void OnClose()
        {
            Clients.Remove(this);
            Clients.Broadcast($"{_name} has gone away.");
        }
    }
}