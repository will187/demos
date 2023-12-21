using Domain.Entities;
using MediatR;
using AutoMapper;
using Shared;
using static Application.Common.Mappings.IMapFrom;
using Application.Interfaces.Interfaces;

namespace Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<Result<int>>, IMapFrom<Customer>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}
internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = new Customer()
        {
            Name = command.Name,
            Address = command.Address,
            Phone = command.Phone
        };

        await _unitOfWork.Repository<Customer>().AddAsync(customer);
        customer.AddDomainEvent(new CustomerCreatedEvent(customer));
        await _unitOfWork.Save(cancellationToken);
        return await Result<int>.SuccessAsync(customer.Id, "Customer Created.");
    }
}
