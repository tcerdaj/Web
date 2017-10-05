using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace JehovaJireh.Web.UI.Hubs
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

       
        public void Send(string json, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(json, message);
        }

        public void SendMessageToUser(string userId, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.User(userId).send(message);
        }

        public void SendChatMessage(string who, string message)
        {
            string name = Context.User.Identity.Name;
            Clients.Group(name).addChatMessage(name, message);
            Clients.Group("2@2.com").addChatMessage(name, message);
        }

        public override Task OnConnected()
        {
            if (Context.User != null)
            {
                string name = Context.User.Identity.Name;
                Groups.Add(Context.ConnectionId, name);
            }

            return base.OnConnected();
        }
    }
}