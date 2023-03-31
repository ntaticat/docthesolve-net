using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.User;

public class CreateUserCommand
{
    public record CreateUserCommandDto : IRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class Handler : IRequestHandler<CreateUserCommandDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Handler(UserManager<Domain.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<Unit> Handle(CreateUserCommandDto request, CancellationToken cancellationToken)
        {
            var user = new Domain.ApplicationUser
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Couldn't create the user");
            }
            
            return Unit.Value;
        }
    }
}