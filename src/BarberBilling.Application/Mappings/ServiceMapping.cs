using BarberBilling.Application.Mappings.Common;
using BarberBilling.Application.Resources;
using BarberBilling.Application.UseCases.Categories.GetAll;
using BarberBilling.Communication.Requests.Services;
using BarberBilling.Communication.Requests.Services.GetAllFilter;
using BarberBilling.Communication.Responses.Categories;
using BarberBilling.Communication.Responses.Services;
using BarberBilling.Communication.Responses.Services.Delete;
using BarberBilling.Communication.Responses.Services.Register;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Filters;
using BarberBilling.Domain.Enums;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.Mappings;
public static class ServiceMapping
{
    public static Service ToEntity(this RequestServiceJson request)
    {
        return new Service
        {
            Id = Guid.NewGuid(),
            Services = (Services)request.Services,
            Price = request.Price,
            Category = (Category)request.Category,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static ResponseRegisterServiceJson ToRegisterResponse(this Service service)
    {
        return new ResponseRegisterServiceJson
        {
            ServiceIdentifier = service.Id
        };
    }

    public static ResponseServiceJson ToResponse(this Service service, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return new ResponseServiceJson
        (
            ServiceIdentifier: service.Id,
            Id: Convert.ToInt32(service.Services),
            Description: localizer![service.Services.ToString()],
            Price: service.Price
        );
    }

    public static ResponseServicesJson ToResponse(this List<Service> services, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return new ResponseServicesJson
        {
            Services = services.Select(s => s.ToResponse(localizer)).ToList()
        };
    }

    public static List<ResponseServiceJson> ToGetAllResponse(this List<Service> entities,
    IStringLocalizer<ResourceEnumResponse>? localizer)
    {
        return entities.Select(entity => new ResponseServiceJson
        (
            ServiceIdentifier: entity.Id,
            Id: Convert.ToInt32(entity.Services),
            Description: localizer![entity.Services.ToString()],
            Price: entity.Price
        )).ToList();
    }

    public static List<ResponseCategoryJson> ToResponseCategory(this List<Category> entities, IStringLocalizer<ResourceEnumResponse>? localizer)
    {
        return entities.Select(entity => new ResponseCategoryJson
        (
            Id: Convert.ToInt32(entity),
            Description: localizer![entity.ToString()]
        )).ToList();
    }

    public static ServiceFilter ToFilter(this ServiceFilterQuery filter)
    {
        return new ServiceFilter
        {
            Page = filter.Page,
            PageSize = filter.PageSize,
            Order = filter.Order,
            SortBy = filter.SortBy
        };
    }

    public static ResponseSoftDeleteJson ToResponseSoftDelete(this Service entity)
    {
        return new ResponseSoftDeleteJson(
            entity.Id,
            (Communication.Enums.Services)entity.Services,
            entity.IsDeleted
        );
    }
}