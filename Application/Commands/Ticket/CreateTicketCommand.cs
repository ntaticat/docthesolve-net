using Application.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Persistence;

namespace Application.Commands.Ticket;

public class CreateTicketCommand
{
    public record CreateTicketCommandDto : IRequest
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public int Watchers { get; set; }
        public bool SelectedByAgent { get; set; }
        public bool Solved { get; set; }
    }
    
    public class Handler : IRequestHandler<CreateTicketCommandDto>
    {
        private readonly DocTheSolveNetContext _context;
        private readonly IHubContext<IncidenceHub> _incidenceHub;

        public Handler(DocTheSolveNetContext context, IHubContext<IncidenceHub> incidenceHub)
        {
            _context = context;
            _incidenceHub = incidenceHub;
        }
        
        public async Task<Unit> Handle(CreateTicketCommandDto request, CancellationToken cancellationToken)
        {
            var ticket = new Domain.Ticket
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                LongDescription = request.LongDescription,
                Watchers = request.Watchers,
                SelectedByAgent = request.SelectedByAgent,
                Solved = request.Solved
            };

            await _context.Tickets.AddAsync(ticket, cancellationToken);

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                await _incidenceHub.Clients.All.SendAsync("ReceiveMessage", ticket, cancellationToken: cancellationToken);
                return Unit.Value;
            }

            throw new Exception("Couldn't create the ticket");
        }
    }
}