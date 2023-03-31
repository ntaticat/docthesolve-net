using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User.Agent;

public class GetUserByIdQuery
{
    public record GetUserByIdQueryDto : IRequest<Domain.ApplicationUser>
    {
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<GetUserByIdQueryDto, Domain.ApplicationUser>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.ApplicationUser> Handle(GetUserByIdQueryDto request, CancellationToken cancellationToken)
        {
            var user = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken:cancellationToken);

            if (user == null)
            {
                throw new Exception("Error al encontrar al usuario");
            }

            return user;
        }
    }
}