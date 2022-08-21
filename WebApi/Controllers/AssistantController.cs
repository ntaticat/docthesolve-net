
using Application.Commands;
using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssistantController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssistantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<Assistant> GetAssistant(Guid id)
    {
        return await _mediator.Send(new GetAssistantQuery.Query { Id = id});
    }
    
    [HttpPost()]
    public async Task<Unit> PostAssistant([FromBody] CreateAssistantCommand.AssistantInfoCommand data)
    {
        return await _mediator.Send(data);
    }
}