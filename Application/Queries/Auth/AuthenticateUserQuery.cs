using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Queries.Auth;

public class AuthenticateUserQuery
{
    public record AuthenticateUserQueryDto : IRequest<ResponseCredentialsDto>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    
    public class Handler : IRequestHandler<AuthenticateUserQueryDto, ResponseCredentialsDto>
    {
        private readonly SignInManager<Domain.ApplicationUser> _signInManager;
        private readonly UserManager<Domain.ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public Handler(SignInManager<Domain.ApplicationUser> signInManager, UserManager<Domain.ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        
        public async Task<ResponseCredentialsDto> Handle(AuthenticateUserQueryDto request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("User or password incorrect");
            }

            var passwordChecked =
                await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!passwordChecked.Succeeded)
            {
                throw new Exception("User or password incorrect");
            }

            return CreateToken(user);
        }

        private ResponseCredentialsDto CreateToken(Domain.ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim("usuarioId", user.Id.ToString()),
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(6);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration,
                signingCredentials: credentials);

            return new ResponseCredentialsDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };
        }
    }
}