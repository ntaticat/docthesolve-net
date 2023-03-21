using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Agent;

public class GetAgentByIdQuery
{
    public record GetAgentByIdQueryDto : IRequest<Domain.Agent>
    {
        public Guid AgentId { get; set; }
    }

    public class Handler : IRequestHandler<GetAgentByIdQueryDto, Domain.Agent>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.Agent> Handle(GetAgentByIdQueryDto request, CancellationToken cancellationToken)
        {
            var agent = await _context.Agents.SingleOrDefaultAsync(x => x.AgentId == request.AgentId);

            if (agent == null)
            {
                throw new Exception("Error al encontrar al agente");
            }

            return agent;
        }
    }
}