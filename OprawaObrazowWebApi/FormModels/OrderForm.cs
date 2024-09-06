using System.ComponentModel.DataAnnotations;
using OprawaObrazowWebApi.CustomDataAnnotations;

namespace OprawaObrazowWebApi.FormModels;

public class OrderForm
{
    public int Id { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required, FutureDate]
    public DateTime DateDue { get; set; }
    
    [Required]
    public int ClientId { get; set; }

    [Required]
    public int PictureWidth { get; set; }

    [Required]
    public int PictureHeight { get; set; }

    public List<FramePieceForm> FramePieces { get; set; } = new();
}