using Domain;
using MediatR;
using Persistence;

namespace Application.Commands.Agent;

public class CreateAgentCommand
{
    public record CreateAgentCommandDto : IRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class Handler : IRequestHandler<CreateAgentCommandDto>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(CreateAgentCommandDto request, CancellationToken cancellationToken)
        {
            var assistant = new Domain.Agent
            {
                Email = request.Email,
                Password = request.Password
            };

            await _context.Agents.AddAsync(assistant);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Unit.Value;
            }

            throw new Exception("Error");
        }
    }
}