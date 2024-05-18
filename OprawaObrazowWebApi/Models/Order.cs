using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.Models;

public partial class Order
{
    public int Id { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public DateOnly DateDue { get; set; }

    [Required]
    public bool IsFinished { get; set; }

    [Required]
    public bool IsClosed { get; set; }

    public DateOnly? PlannedDate { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    public int PictureWidth { get; set; }

    [Required]
    public int PictureHeight { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<FramePiece> FramePieces { get; set; } = new List<FramePiece>();
}
