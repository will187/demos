using Application.Interfaces.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}

internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(command.Id);
        if (customer != null)
        {
            command.Name = customer.Name;
            command.Address = customer.Address;
            command.Phone = customer.Phone;

            await _unitOfWork.Repository<Customer>().UpdateAsync(customer);
            customer.AddDomainEvent(new UpdateCustomerEvent(customer));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(customer.Id, "Customer Updated");
        }
        else
        {
            return await Result<int>.FailureAsync("Customer Not Found");
        }
    }
}

