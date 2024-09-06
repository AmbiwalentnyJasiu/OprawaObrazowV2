using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.FormModels;

public class FrameForm
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
    public string Code { get; set; }
    
    public List<FramePieceForm> FramePieces { get; set; } = new();
}