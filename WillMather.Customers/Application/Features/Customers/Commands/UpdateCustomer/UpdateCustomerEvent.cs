using Domain.Common;
using Domain.Entities;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerEvent : BaseEvent
{
    public Customer Customer { get; }
    public UpdateCustomerEvent(Customer customer)
    {
        Customer = customer;
    }
}
