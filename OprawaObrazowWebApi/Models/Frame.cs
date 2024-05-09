namespace OprawaObrazowWebApi.Models;

public partial class Frame
{
    public int Id { get; set; }

    public int Color { get; set; }

    public decimal Price { get; set; }

    public int SupplierId { get; set; }

    public int Width { get; set; }

    public bool HasDecoration { get; set; }

    public string? StorageLocation { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<FramePiece> FramePieces { get; set; } = new List<FramePiece>();

    public virtual Supplier Supplier { get; set; } = null!;
}
