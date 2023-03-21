using Domain;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public class IncidenceHub : Hub
{
    public async Task SendMessage(Ticket ticket)
        => await Clients.All.SendAsync("ReceiveMessage", ticket);
}