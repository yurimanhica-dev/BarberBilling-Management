using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Exceptions.ExceptionsBase;

namespace BarberBilling.Application.UseCases.Billings.Delete;

public class DeleteBillingUseCase : IDeleteBillingUseCase
{
    private readonly IBillingWriteOnlyRepository _billingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBillingUseCase(
        IBillingWriteOnlyRepository billing,
        IUnitOfWork unitOfWork)
    {
        _billingRepository = billing;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid Id)
    {
        var result = await _billingRepository.Delete(Id);

        if (!result)
            throw new NotFoundException("BillingNotFound");

        await _unitOfWork.Commit();
    }
}