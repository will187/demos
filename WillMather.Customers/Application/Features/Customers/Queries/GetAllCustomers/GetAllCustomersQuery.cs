using Application.Interfaces.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery : IRequest<Result<List<GetAllCustomersDto>>>;

internal class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result<List<GetAllCustomersDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<List<GetAllCustomersDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().Entities.ProjectTo<GetAllCustomersDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        return await Result<List<GetAllCustomersDto>>.SuccessAsync(customer);
    }
}
