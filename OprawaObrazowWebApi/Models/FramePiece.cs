using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.Models;

public partial class FramePiece
{
    public int Id { get; set; }

    [Required]
    public int Length { get; set; }

    [Required]
    public bool IsDamaged { get; set; }

    [Required]
    public int FrameId { get; set; }
    
    [MaxLength(10)]
    public string? StorageLocation { get; set; }

    [Required]
    public bool IsInStock { get; set; }

    [Required]
    public int DeliveryId { get; set; }
    
    public int? OrderId { get; set; }

    public virtual Delivery Delivery { get; set; } = null!;

    public virtual Frame Frame { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
