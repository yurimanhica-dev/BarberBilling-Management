using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Responses.Billings.GetAll;
using BarberBilling.Domain.Repositories.Billings;

namespace BarberBilling.Application.UseCases.Billings.GetAll;

public class GetAllBillingUseCase(IBillingReadOnlyRepository billing) : IGetAllBillingUseCase
{
    private readonly IBillingReadOnlyRepository _billingRepository = billing;
    public async Task<ResponseBillingsJson> Execute()
    {
        var result = await _billingRepository.GetAll();
        
        return new ResponseBillingsJson
        {
            Billings = result.ToGetAllResponse()
        };
    }
}