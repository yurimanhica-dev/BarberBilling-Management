using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Responses.Services;
using BarberBilling.Domain.Entities.Filters;
using BarberBilling.Domain.Repositories.Services;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Services.GetAll;

public class GetAllServicesUseCase : IGetAllServicesUseCase
{
    private readonly IServiceReadOnlyRepository _readRepository;
    private readonly IStringLocalizer<ResourceEnumResponse> _statusLocalizer;
    public GetAllServicesUseCase(IServiceReadOnlyRepository readRepository, IStringLocalizer<ResourceEnumResponse> statusLocalizer)
    {
        _readRepository = readRepository;
        _statusLocalizer = statusLocalizer;
    }

    public async Task<ResponseServicesJson> Execute(ServiceFilter filter)
    {
        var (services, totalCount) = await _readRepository.GetAll(filter);

        return new ResponseServicesJson
        {
            Services = services.ToGetAllResponse(_statusLocalizer),
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize)
        };
    }
}