namespace OprawaObrazowWebApi.Models;

public partial class Delivery
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public DateOnly DateDue { get; set; }

    public bool IsDelivered { get; set; }

    public virtual ICollection<FramePiece> FramePieces { get; set; } = new List<FramePiece>();

    public virtual Supplier Supplier { get; set; } = null!;
}
