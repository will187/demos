using Domain.Entities;
using static Application.Common.Mappings.IMapFrom;


namespace Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersDto : IMapFrom<Customer>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
