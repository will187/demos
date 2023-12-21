using Application.Common.Mappings;
using Application.Interfaces.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using static Application.Common.Mappings.IMapFrom;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand : IRequest<Result<int>>, IMapFrom<Customer>
{
    public int Id { get; set; }
    public DeleteCustomerCommand() { }
    public DeleteCustomerCommand(int id) 
    { 
        Id = id;
    }

}

internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(command.Id);
        if(customer != null)
        {
            await _unitOfWork.Repository<Customer>().DeleteAsync(customer);
            customer.AddDomainEvent(new CustomerDeletedEvent(customer));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(customer.Id, "Customer Deleted");
        }
        else 
        {
            return await Result<int>.FailureAsync("Player Not Found");
        }
    }
}
