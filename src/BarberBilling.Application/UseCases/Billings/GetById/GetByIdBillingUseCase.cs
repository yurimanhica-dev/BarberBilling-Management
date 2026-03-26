using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Enums;
using BarberBilling.Communication.Responses.Billings.GetById;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Exceptions.ExceptionsBase;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Billings.GetById;

public class GetByIdBillingUseCase : IGetByIdBillingUseCase
{
    private readonly IBillingReadOnlyRepository _billingRepository;
    private readonly IStringLocalizer<ResourceEnumResponse> _statusLocalizer;
    public GetByIdBillingUseCase(IBillingReadOnlyRepository billingRepository, IStringLocalizer<ResourceEnumResponse> statusLocalizer)
    {
        _billingRepository = billingRepository;
        _statusLocalizer = statusLocalizer;
    }

    public async Task<ResponseBillingJson> Execute(Guid id)
    {
        var result = await _billingRepository.GetById(id) ?? throw new NotFoundException("BillingNotFound");

        var response = result.ToGetByIdResponse(_statusLocalizer);

        return response;
    }
}