using MediatR;
using Persistence;

namespace Application.Commands.Problem;

public class CreateProblemCommand
{
    public record CreateProblemCommandDto : IRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid AgentId { get; set; }
    }

    public class Handler : IRequestHandler<CreateProblemCommandDto>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(CreateProblemCommandDto request, CancellationToken cancellationToken)
        {
            var problem = new Domain.Problem
            {
                Title = request.Title,
                Content = request.Content,
                AgentId = request.AgentId
            };

            await _context.Problems.AddAsync(problem, cancellationToken);

            var result = await _context.SaveChangesAsync(cancellationToken);
            
            if (result > 0)
            {
                return Unit.Value;
            }
            
            throw new Exception("Couldn't create the problem");
        }
    }
}