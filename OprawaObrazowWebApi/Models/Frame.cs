using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.Models;

public partial class Frame
{
    public int Id { get; set; }

    [Required]
    public int Color { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public int Width { get; set; }

    [Required]
    public bool HasDecoration { get; set; }

    [Required, MaxLength(20)]
    public string Code { get; set; } = null!;

    public virtual ICollection<FramePiece> FramePieces { get; set; } = new List<FramePiece>();

    public virtual Supplier Supplier { get; set; } = null!;
}
