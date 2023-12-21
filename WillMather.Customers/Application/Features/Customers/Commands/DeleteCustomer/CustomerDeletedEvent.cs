using Domain.Common;
using Domain.Entities;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public class CustomerDeletedEvent : BaseEvent
{
    public Customer Customer { get; }
    public CustomerDeletedEvent(Customer customer)
    {
        Customer = customer;
    }
}
