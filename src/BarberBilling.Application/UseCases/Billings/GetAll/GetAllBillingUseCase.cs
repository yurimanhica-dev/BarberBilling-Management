using BarberBilling.Application.Mappings;
using BarberBilling.Domain.Repositories.Billings;

namespace BarberBilling.Application.UseCases.Billings.GetAll;

public class GetAllBillingUseCase(IBillingReadOnlyRepository billing) : IGetAllBillingUseCase
{
    private readonly IBillingReadOnlyRepository _billingRepository = billing;
    public async Task<List<GetAllBillingOutput>> Execute()
    {
        var result = await _billingRepository.GetAll();
        return result.ToGetAllOutputs();
    }
}