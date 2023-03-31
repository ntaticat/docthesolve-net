using Application.Dtos.Auth;
using Application.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ResponseCredentialsDto> Authenticate([FromBody] AuthenticateUserQuery.AuthenticateUserQueryDto data)
    {
        return await _mediator.Send(data);
    }
}