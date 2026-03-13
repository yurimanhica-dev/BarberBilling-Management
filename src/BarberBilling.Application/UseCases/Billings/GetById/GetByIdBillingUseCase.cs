using BarberBilling.Application.Mappings;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Exceptions.Base;

namespace BarberBilling.Application.UseCases.Billings.GetById;

public class GetByIdBillingUseCase(IBillingReadOnlyRepository billing) : IGetByIdBillingUseCase
{
    private readonly IBillingReadOnlyRepository _billingRepository = billing;

    public async Task<GetByIdBillingOutput> Execute(Guid id)
    {
        var result = await _billingRepository.GetById(id) ?? throw new NotFoundException("BillingNotFound");
        var response = result.ToGetByIdOutput();
        return response;
    }
}