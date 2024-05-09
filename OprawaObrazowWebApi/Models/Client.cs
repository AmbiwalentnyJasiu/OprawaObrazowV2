namespace OprawaObrazowWebApi.Models;

public partial class Client
{
    public string FirstName { get; set; } = null!;

    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
