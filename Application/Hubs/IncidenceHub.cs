using Domain;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public class IncidenceHub : Hub
{
    public async Task SendMessage(Incidence incidence)
        => await Clients.All.SendAsync("ReceiveMessage", incidence);
}