using System.ComponentModel.DataAnnotations;
using OprawaObrazowWebApi.CustomDataAnnotations;

namespace OprawaObrazowWebApi.FormModels;

public class DeliveryForm
{
    public int Id { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required, FutureDate]
    public DateTime DateDue { get; set; }
    
    public List<FramePieceForm> FramePieces { get; set; } = new();
}