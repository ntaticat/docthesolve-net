
using Application.Commands;
using Application.Commands.User;
using Application.Queries;
using Application.Queries.User;
using Application.User.Agent;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<List<ApplicationUser>> GetUsers()
    {
        return await _mediator.Send(new GetUsersQuery.GetUsersQueryDto());
    }
    
    [HttpGet("{userId}")]
    public async Task<ApplicationUser> GetUser(Guid userId)
    {
        return await _mediator.Send(new GetUserByIdQuery.GetUserByIdQueryDto { UserId = userId });
    }
    
    [HttpPost]
    public async Task<Unit> PostUser([FromBody] CreateUserCommand.CreateUserCommandDto data)
    {
        return await _mediator.Send(data);
    }
}