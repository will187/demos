using Domain.Common;
using Domain.Entities;

namespace Application.Features.Customers.Commands.CreateCustomer;

public class CustomerCreatedEvent : BaseEvent
{
    public Customer Customer { get; }

    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;
    }
}
