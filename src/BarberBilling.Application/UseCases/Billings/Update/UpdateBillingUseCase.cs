using BarberBilling.Application.Mappings;
using BarberBilling.Application.Validators;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Exceptions.Base;

namespace BarberBilling.Application.UseCases.Billings.Update;

public class UpdateBillingUseCase : IUpdateBillingUseCase
{
    private readonly IBillingUpdateOnlyRepository _billingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBillingUseCase(
        IBillingUpdateOnlyRepository billingRepository,
        IUnitOfWork unitOfWork)
    {
        _billingRepository = billingRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid id, BillingInput input)
    {
        new BillingValidator().ValidateInput(input);

        var billing = await _billingRepository.GetById(id) ?? throw new NotFoundException("BillingNotFound");
        input.UpdateEntity(billing);

        _billingRepository.Update(billing);
        await _unitOfWork.Commit();
    }

}