using System.ComponentModel.DataAnnotations;

namespace MovieShop.ApplicationCore.Validators;

public class NotPastPurchaseDateAttribute : ValidationAttribute
{
    public NotPastPurchaseDateAttribute()
    {
        ErrorMessage = "Purchase date cannot be earlier than today's date.";
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is not DateTime purchaseDate)
        {
            return false;
        }

        if (purchaseDate == default)
        {
            return true;
        }

        return purchaseDate.Date >= DateTime.UtcNow.Date;
    }
}
