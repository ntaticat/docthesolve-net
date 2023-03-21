using Application.Commands.Customer;
using Application.Queries.Customer;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController: ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<Customer>> GetCustomers()
    {
        return await _mediator.Send(new GetCustomersQuery.GetCustomersQueryDto());
    }
    
    [HttpPost]
    public async Task<Unit> PostCustomer([FromBody] CreateCustomerCommand.CreateCustomerCommandDto data)
    {
        return await _mediator.Send(data);
    }
}