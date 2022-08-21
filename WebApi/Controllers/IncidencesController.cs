using Application.Commands;
using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncidencesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IncidencesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<List<Incidence>> GetIncidenceList()
    {
        return await _mediator.Send(new GetIncidencesQuery.Query());
    }
    
    [HttpPost()]
    public async Task<Unit> PostIncidence([FromBody] CreateIncidenceCommand.IncidenceInfoCommand data)
    {
        return await _mediator.Send(data);
    }
}
