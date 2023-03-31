using Application.Commands.Problem;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize(JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class ProblemsController: ControllerBase
{
    private readonly IMediator _mediator;

    public ProblemsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Unit> PostProblem([FromBody] CreateProblemCommand.CreateProblemCommandDto data)
    {
        return await _mediator.Send(data);
    }
}