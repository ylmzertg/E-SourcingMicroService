using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Hubs
{
    public class AuctionHub : Hub
    {
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendBid(string groupName, string user, string bid)
        {
            await Clients.Group(groupName).SendAsync("Bids", user, bid);
        }
    }
}
