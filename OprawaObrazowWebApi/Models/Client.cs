using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.Models;

public partial class Client
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [MaxLength(15), Phone]
    public string? PhoneNumber { get; set; }

    [MaxLength(50), EmailAddress]
    public string? EmailAddress { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
