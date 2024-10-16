using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using TaskFlow.Entities.Models;

namespace DemoProject.Hubs
{
    public class ConnectionHub:Hub
    {

        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveConnectInfo", "User Connected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("DisconnectInfo", "User disconnected");

        }
    }
}
