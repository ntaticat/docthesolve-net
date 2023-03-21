using Application.Commands;
using Application.Commands.Ticket;
using Application.Queries;
using Application.Queries.Ticket;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<List<Ticket>> GetTickets()
    {
        return await _mediator.Send(new GetTicketsQuery.GetTicketsQueryDto());
    }
    
    [HttpPost()]
    public async Task<Unit> PostTicket([FromBody] CreateTicketCommand.CreateTicketCommandDto data)
    {
        return await _mediator.Send(data);
    }
}
