using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Enums;
using BarberBilling.Communication.Responses.Billings.GetAll;
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
    public async Task<ResponseBillingsJson> Execute()
    {
        var result = await _billingRepository.GetAll();
        
        return new ResponseBillingsJson
        {
            Billings = result.ToGetAllResponse(_statusLocalizer)
        };
    }
}