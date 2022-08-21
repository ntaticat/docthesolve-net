using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Queries;

public class GetAssistantQuery
{
    public record Query : IRequest<Assistant>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Assistant>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<Assistant> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Assistants.SingleOrDefaultAsync(x => x.AssistantId == request.Id);
        }
    }
}