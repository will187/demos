using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Commands.DeleteCustomer;
using Application.Features.Customers.Commands.UpdateCustomer;
using Application.Features.Customers.Queries.GetAllCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers;

public class CustomerController :ApiBaseController
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<GetAllCustomersDto>>>> Get()
    {
        return await _mediator.Send(new GetAllCustomersQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Result<int>>> Create(CreateCustomerCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<int>>> Update(int id, UpdateCustomerCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<int>>> Delete(int id)
    {
        return await _mediator.Send(new DeleteCustomerCommand(id));
    }
}
