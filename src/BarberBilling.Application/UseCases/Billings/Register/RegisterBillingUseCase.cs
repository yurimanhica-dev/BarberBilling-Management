using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Application.Validators;
using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses.Billings.Register;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Exceptions.ExceptionsBase;

namespace BarberBilling.Application.UseCases.Billings.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingWriteOnlyRepository _billingWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBillingUseCase(IBillingWriteOnlyRepository expensesRepository,
    IUserReadOnlyRepository userReadOnlyRepository,IUnitOfWork unitOfWork)
    {
        _billingWriteOnlyRepository = expensesRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterBillingJson> Execute(BillingRequestJson request, Guid userId)
    {
        new BillingValidator().ValidateInput(request);

        var user = await _userReadOnlyRepository.GetByIdentifier(userId);

        var entity = request.ToEntity();
        entity.BarberIdentifier = user!.UserIdentifier;
        entity.CreatedAt = DateTime.UtcNow;

        await _billingWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();

        return entity.ToRegisterResponse();
    }
}