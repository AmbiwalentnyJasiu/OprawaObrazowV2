using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.Models;

public partial class Delivery
{
    public int Id { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public DateOnly DateDue { get; set; }

    [Required]
    public bool IsDelivered { get; set; }

    public virtual ICollection<FramePiece> FramePieces { get; set; } = new List<FramePiece>();

    public virtual Supplier Supplier { get; set; } = null!;
}
