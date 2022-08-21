using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Queries;

public class GetIncidencesQuery
{
    public record Query :  IRequest<List<Incidence>> {}

    public class Handler : IRequestHandler<Query, List<Incidence>>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<List<Incidence>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Incidences.ToListAsync();
        }
    }
}