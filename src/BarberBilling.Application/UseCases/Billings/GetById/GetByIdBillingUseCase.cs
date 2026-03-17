using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Responses.Billings.GetById;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Exceptions.Base;

namespace BarberBilling.Application.UseCases.Billings.GetById;

public class GetByIdBillingUseCase(IBillingReadOnlyRepository billing) : IGetByIdBillingUseCase
{
    private readonly IBillingReadOnlyRepository _billingRepository = billing;

    public async Task<ResponseBillingJson> Execute(Guid id)
    {
        var result = await _billingRepository.GetById(id) ?? throw new NotFoundException("BillingNotFound");
        var response = result.ToGetByIdResponse();
        return response;
    }
}