using BarberBilling.Application.Resources;
using BarberBilling.Domain.Enums;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.Extensions;

public static class PaymentMethodExtensions
{
    public static string GetDescription(this PaymentMethod paymentMethod, IStringLocalizer<ResourceEnumResponse> enumLocalizer)
    {
        return paymentMethod switch
        {
            PaymentMethod.Cash => enumLocalizer["Cash"],
            PaymentMethod.CreditCard => enumLocalizer["CreditCard"],
            PaymentMethod.DebitCard => enumLocalizer["DebitCard"],
            PaymentMethod.BankTransfer => enumLocalizer["BankTransfer"],
            PaymentMethod.DigitalWallet => enumLocalizer["DigitalWallet"],
            PaymentMethod.MobileWallet => enumLocalizer["MobileWallet"],
            _ => enumLocalizer["Other"]
        };
    }
}