using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.User;

public class GetUsersQuery
{
    public record GetUsersQueryDto: IRequest<List<Domain.ApplicationUser>> {}
    
    public class Handler : IRequestHandler<GetUsersQueryDto, List<Domain.ApplicationUser>>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<List<Domain.ApplicationUser>> Handle(GetUsersQueryDto request, CancellationToken cancellationToken)
        {
            var customers = await _context.ApplicationUsers.ToListAsync(cancellationToken: cancellationToken);

            return customers;
        }
    }
}