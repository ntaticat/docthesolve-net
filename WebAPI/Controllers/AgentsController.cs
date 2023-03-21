
using Application.Commands;
using Application.Commands.Agent;
using Application.Queries;
using Application.Queries.Agent;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{agentId}")]
    public async Task<Agent> GetAgent(Guid agentId)
    {
        return await _mediator.Send(new GetAgentByIdQuery.GetAgentByIdQueryDto { AgentId = agentId});
    }
    
    [HttpPost()]
    public async Task<Unit> PostAgent([FromBody] CreateAgentCommand.CreateAgentCommandDto data)
    {
        return await _mediator.Send(data);
    }
}