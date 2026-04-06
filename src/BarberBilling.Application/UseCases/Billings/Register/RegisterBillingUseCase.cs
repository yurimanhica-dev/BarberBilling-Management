using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses.Billings.Register;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Exceptions.CustomExceptions;
using BarberBilling.Domain.Repositories.Services;

namespace BarberBilling.Application.UseCases.Billings.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IServiceReadOnlyRepository _serviceReadOnlyRepository;
    private readonly IBillingWriteOnlyRepository _billingWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBillingUseCase(IBillingWriteOnlyRepository expensesRepository,
    IServiceReadOnlyRepository serviceReadOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,IUnitOfWork unitOfWork)
    {
        _billingWriteOnlyRepository = expensesRepository;
        _serviceReadOnlyRepository = serviceReadOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterBillingJson> Execute(BillingRequestJson request, Guid userId)
    {
        new BillingValidator().ValidateInput(request);

        var user = await _userReadOnlyRepository.GetByIdentifier(userId);

        var client = await _userReadOnlyRepository.GetByIdentifier(request.ClientIdentifier)
            ?? throw new NotFoundException("UserNotFound");

        var entity = request.ToEntity();
        entity.BarberIdentifier = user!.UserIdentifier;
        entity.ClientIdentifier = client.UserIdentifier;
        entity.CreatedAt = DateTime.UtcNow;

        // Busca cada serviço e faz o snapshot
        foreach (var serviceId in request.ServiceIds)
        {
            var service = await _serviceReadOnlyRepository.GetByIdentifier(serviceId)
                ?? throw new NotFoundException($"ServiceNotFound");

            entity.Services.Add(service.ToServiceEntity(entity.Id));
        }

        // Calcula o total automaticamente
        entity.TotalAmount = entity.Services.Sum(s => s.Price);

        await _billingWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();

        return entity.ToRegisterResponse();
    }
}