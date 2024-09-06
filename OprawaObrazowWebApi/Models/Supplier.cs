using System.ComponentModel.DataAnnotations;
using OprawaObrazowWebApi.FormModels;

namespace OprawaObrazowWebApi.Models;

public partial class Supplier
{
    public Supplier() { }

    public Supplier(SupplierForm form)
    {
        Id = form.Id;
        Name = form.Name;
        LastSupply = form.LastSupply;
        PhoneNumber = form.PhoneNumber;
        EmailAddress = form.EmailAddress;
    }
    
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? LastSupply { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
    public virtual ICollection<Frame> Frames { get; set; } = new List<Frame>();
}
