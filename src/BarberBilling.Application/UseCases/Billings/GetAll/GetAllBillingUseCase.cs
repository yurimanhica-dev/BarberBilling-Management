using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Responses.Billings.GetAll;
using BarberBilling.Domain.Entities.QueryParameters;
using BarberBilling.Domain.Repositories.Billings;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Billings.GetAll;

public class GetAllBillingUseCase : IGetAllBillingUseCase
{
    private readonly IStringLocalizer<ResourceEnumResponse> _statusLocalizer;
    private readonly IBillingReadOnlyRepository _billingRepository;
    public GetAllBillingUseCase(IBillingReadOnlyRepository billing, IStringLocalizer<ResourceEnumResponse> statusLocalizer)
    {
        _billingRepository = billing;
        _statusLocalizer = statusLocalizer;
    }
    public async Task<ResponseBillingsJson> Execute(BillingFilter filter, Guid userId, string role)
    {
        var (billings, totalCount) = await _billingRepository.GetAll(filter, userId, role);

        return new ResponseBillingsJson
        {
            Billings = billings.ToGetAllResponse(_statusLocalizer),
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize)
        };
    }
}