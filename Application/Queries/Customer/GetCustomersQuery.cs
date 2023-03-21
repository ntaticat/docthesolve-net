using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Customer;

public class GetCustomersQuery
{
    public record GetCustomersQueryDto: IRequest<List<Domain.Customer>> {}
    
    public class Handler : IRequestHandler<GetCustomersQueryDto, List<Domain.Customer>>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }
        
        public async Task<List<Domain.Customer>> Handle(GetCustomersQueryDto request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.ToListAsync(cancellationToken: cancellationToken);

            return customers;
        }
    }
}