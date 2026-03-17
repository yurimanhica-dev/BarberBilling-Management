using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Application.Validators;
using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses.Billings.Register;

namespace BarberBilling.Application.UseCases.Billings.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingWriteOnlyRepository _expensesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBillingUseCase(IBillingWriteOnlyRepository expensesRepository, IUnitOfWork unitOfWork)
    {
        _expensesRepository = expensesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterBillingJson> Execute(BillingRequestJson request)
    {
        new BillingValidator().ValidateInput(request);

        var entity = request.ToEntity();
        entity.CreatedAt = DateTime.UtcNow;

        await _expensesRepository.Add(entity);
        await _unitOfWork.Commit();

        return entity.ToRegisterResponse();
    }
}