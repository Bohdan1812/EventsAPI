using Domain.ChatAggregate.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string eventId, string message) 
        {
            await Clients.Group(eventId).SendAsync("MessageReceived", message); //OthersInGroup(chatId)
            //Don't forget tp exclude current user from sending after tests
        }

        public async Task JoinGroup(string eventId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, eventId);
        }

        public async Task RemoveFromGroup(string eventId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, eventId);
        }
    }
}
