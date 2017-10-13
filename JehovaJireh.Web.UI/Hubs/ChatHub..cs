using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using JehovaJireh.Web.UI.Models;
using System.Web.Helpers;

namespace JehovaJireh.Web.UI.Hubs
{
    public class ChatHub : Hub
    {
        static List<UserConnection> SignalRUsers = new List<UserConnection>();
        const string roomName = "chat";
        static string userObject;
        static dynamic user;
        public void Hello()
        {
            Clients.All.hello();
        }

       
        public void Send(string json, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //user = Json.Decode(json);
            //user.Members = SignalRUsers.ToList();
            Clients.Group(roomName).addNewMessageToPage(json, message);
        }

        public void SendChatMessage(string connectionId, string who, string message)
        {
            string name = Context.User.Identity.Name;
            Clients.Group(roomName).Client(connectionId).addChatMessage(who, message);
        }

        public override Task OnConnected()
        {
            userObject = Context.QueryString["userobject"];
            user = Json.Decode(userObject);
            user.ConnectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userObject))
            {
                var results = JoinRoom(roomName);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var results = LeaveRoom(roomName);
            return base.OnDisconnected(stopCalled);
        }

        public Task JoinRoom(string roomName)
        {
            user = Json.Decode(userObject);
            var memberName = GetProperty(user, "Name");
            var userAlreadyConnected = SignalRUsers.Any(x =>x.ConnectionId == Context.ConnectionId);

            if (!userAlreadyConnected)
                SignalRUsers.Add(new UserConnection { ConnectionId = Context.ConnectionId, UserObject = userObject });

            Clients.Group(roomName).addNewMessageToPage(userObject, "has entered the room.");
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            var count = SignalRUsers.RemoveAll(x =>x.ConnectionId == Context.ConnectionId);
            Clients.Group(roomName).addNewMessageToPage(userObject, "has left the room.");
            return Groups.Remove(Context.ConnectionId, roomName);
        }

        public List<UserConnection> GetAllActiveConnections()
        {
            return SignalRUsers.ToList();
        }

        public static object GetProperty(object target, string name)
        {
            var site = System.Runtime.CompilerServices.CallSite<Func<System.Runtime.CompilerServices.CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, name, target.GetType(), new[] { Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) }));
            return site.Target(site, target);
        }
    }
}