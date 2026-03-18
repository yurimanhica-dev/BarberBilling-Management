using BarberBilling.Application.Resources;
using BarberBilling.Communication.Responses.Shared;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.Mappings.Common;

    public static class EnumMappingExtensions
    {
        public static EnumResponse ToEnumResponse<TEnum>(
        this TEnum enumValue,
        IStringLocalizer<ResourceEnumResponse> localizer)
        where TEnum : Enum
        {
            return new EnumResponse(
                Id: Convert.ToInt32(enumValue),
                Description: localizer[enumValue.ToString()]
            );
        }
    }
