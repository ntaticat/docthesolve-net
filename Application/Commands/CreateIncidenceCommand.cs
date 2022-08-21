using Application.Hubs;
using Domain;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Persistance;

namespace Application.Commands;

public class CreateIncidenceCommand
{
    public record IncidenceInfoCommand : IRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Watchers { get; set; }
        public bool SelectedByAssistant { get; set; }
        public bool Solved { get; set; }
    }
    
    public class Handler : IRequestHandler<IncidenceInfoCommand>
    {
        private readonly DocTheSolveNetContext _context;
        private readonly IHubContext<IncidenceHub> _incidenceHub;

        public Handler(DocTheSolveNetContext context, IHubContext<IncidenceHub> incidenceHub)
        {
            _context = context;
            _incidenceHub = incidenceHub;
        }
        
        public async Task<Unit> Handle(IncidenceInfoCommand request, CancellationToken cancellationToken)
        {
            var incidence = new Incidence
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                LongDescription = request.LongDescription,
                Watchers = request.Watchers,
                SelectedByAssistant = request.SelectedByAssistant,
                Solved = request.Solved
            };

            await _context.Incidences.AddAsync(incidence);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                await _incidenceHub.Clients.All.SendAsync("ReceiveMessage", incidence);
                return Unit.Value;
            }

            throw new Exception("Error");
        }
    }
}