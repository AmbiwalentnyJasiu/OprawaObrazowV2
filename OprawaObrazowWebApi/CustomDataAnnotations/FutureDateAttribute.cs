using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.CustomDataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime valueDate)
        {
            return valueDate >= DateTime.Today;
        }

        return true;
    }
}