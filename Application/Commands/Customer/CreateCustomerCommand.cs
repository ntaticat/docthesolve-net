using MediatR;
using Persistence;

namespace Application.Commands.Customer;

public class CreateCustomerCommand
{
    public record CreateCustomerCommandDto : IRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    public class Handler : IRequestHandler<CreateCustomerCommandDto>
    {
        private readonly DocTheSolveNetContext _context;

        public Handler(DocTheSolveNetContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCustomerCommandDto request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            await _context.Customers.AddAsync(customer, cancellationToken);

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                return Unit.Value;
            }

            throw new Exception("Couldn't create the customer");
        }
    }
}