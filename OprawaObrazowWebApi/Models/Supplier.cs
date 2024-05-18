using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.Models;

public partial class Supplier
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = null!;

    public DateOnly? LastSupply { get; set; }

    [MaxLength(15), Phone]
    public string? PhoneNumber { get; set; }

    [MaxLength(50), EmailAddress]
    public string? EmailAddress { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Frame> Frames { get; set; } = new List<Frame>();
}
