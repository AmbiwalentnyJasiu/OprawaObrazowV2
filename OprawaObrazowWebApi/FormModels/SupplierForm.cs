using System.ComponentModel.DataAnnotations;
using OprawaObrazowWebApi.CustomDataAnnotations;

namespace OprawaObrazowWebApi.FormModels;

public class SupplierForm
{
    public int Id { get; set; }
    
    [Required, MaxLength(200)]
    public string Name { get; set; }
    
    [FutureDate]
    public DateTime? LastSupply { get; set; }
    
    [MaxLength(15), Phone]
    public string? PhoneNumber { get; set; }
    
    [MaxLength(50), EmailAddress]
    public string? EmailAddress { get; set; }
    
    public List<FrameForm> Frames { get; set; } = new();
}