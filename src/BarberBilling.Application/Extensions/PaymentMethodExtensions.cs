using BarberBilling.Domain.Enums;
using BarberBilling.Domain.Resources;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.Extensions;

public static class PaymentMethodExtensions
{
    public static string GetDescription(this PaymentMethod paymentMethod, IStringLocalizer<ResourceReportGenerationMessages> localizer)
    {
        return paymentMethod switch
        {
            PaymentMethod.Cash => localizer["Cash"],
            PaymentMethod.CreditCard => localizer["CreditCard"],
            PaymentMethod.DebitCard => localizer["DebitCard"],
            PaymentMethod.BankTransfer => localizer["BankTransfer"],
            PaymentMethod.DigitalWallet => localizer["DigitalWallet"],
            PaymentMethod.MobileWallet => localizer["MobileWallet"],
            _ => localizer["Other"]
        };
    }
}