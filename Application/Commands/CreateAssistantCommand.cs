using Domain;
using MediatR;
using Persistance;

namespace Application.Commands;

public class CreateAssistantCommand
{
    public record AssistantInfoCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Handler : IRequestHandler<AssistantInfoCommand>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(AssistantInfoCommand request, CancellationToken cancellationToken)
        {
            var assistant = new Assistant
            {
                Email = request.Email,
                Password = request.Password
            };

            await _context.Assistants.AddAsync(assistant);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Unit.Value;
            }

            throw new Exception("Error");
        }
    }
}