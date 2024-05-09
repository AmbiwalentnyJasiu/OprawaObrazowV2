namespace OprawaObrazowWebApi.Models;

public partial class FramePiece
{
    public int Id { get; set; }

    public int Length { get; set; }

    public bool IsDamaged { get; set; }

    public int FrameId { get; set; }

    public bool IsInStock { get; set; }

    public int DeliveryId { get; set; }

    public int? OrderId { get; set; }

    public virtual Delivery Delivery { get; set; } = null!;

    public virtual Frame Frame { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
