using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
