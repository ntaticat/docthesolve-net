using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Ticket;

public class GetTicketsQuery
{
    public record GetTicketsQueryDto :  IRequest<List<Domain.Ticket>> {}

    public class Handler : IRequestHandler<GetTicketsQueryDto, List<Domain.Ticket>>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<List<Domain.Ticket>> Handle(GetTicketsQueryDto request, CancellationToken cancellationToken)
        {
            return await _context.Tickets.ToListAsync();
        }
    }
}