using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using newsSite.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newsSite.Hubs
{
    public class ChatRoomHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ChatRoomHub(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task RegisterUser(string username)
        {
            var user = (await userManager.FindByEmailAsync(username));
            user.signalRConnectionId = Context.ConnectionId;
            await userManager.UpdateAsync(user);
            List<ApplicationUser> onlineusers =
                userManager.Users.Where(x => x.signalRConnectionId != null).ToList();

            await Clients.All.SendAsync("RefreshOnlineUsers", onlineusers.Select(x => x.UserName).ToList());

            //userManager.UpdateAsync(user).Wait();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var user = userManager.Users.FirstOrDefault(x => x.signalRConnectionId == Context.ConnectionId);
            if (user != null)
            {
                user.signalRConnectionId = null;
                await userManager.UpdateAsync(user);
            }
        }

        public async Task SendMessageClientToServer(string username, string message, string recepient)
        {
            if (recepient == "")
            {
                await Clients.All.SendAsync("SendMessageServerToClient", username, message);
            }
            else
            {
                var user = await userManager.FindByEmailAsync(recepient);
                if (user.signalRConnectionId != null)
                {
                    await Clients.Client(user.signalRConnectionId)
                        .SendAsync("SendMessageServerToClient", username, message);
                }
            }
        }

    }
}
