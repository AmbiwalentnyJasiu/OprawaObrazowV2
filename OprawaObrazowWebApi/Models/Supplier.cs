namespace OprawaObrazowWebApi.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? LastSupply { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Frame> Frames { get; set; } = new List<Frame>();
}
