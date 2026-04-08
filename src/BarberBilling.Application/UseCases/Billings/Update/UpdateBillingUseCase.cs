using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Domain.Repositories.Services;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Billings.Update;

public class UpdateBillingUseCase : IUpdateBillingUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IServiceReadOnlyRepository _serviceReadOnlyRepository;
    private readonly IBillingUpdateOnlyRepository _billingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBillingUseCase(
        IBillingUpdateOnlyRepository billingRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IServiceReadOnlyRepository serviceReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _billingRepository = billingRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _serviceReadOnlyRepository = serviceReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid id, BillingRequestJson request)
    {
        new BillingValidator().ValidateInput(request);

        var billing = await _billingRepository.GetById(id)
            ?? throw new NotFoundException("BillingNotFound");

        // Busca todos os serviços da lista
        var services = new List<Service>();
        foreach (var serviceId in request.ServiceIds)
        {
            var service = await _serviceReadOnlyRepository.GetByIdentifier(serviceId)
                ?? throw new NotFoundException("ServiceNotFound");

            services.Add(service);
        }

        // Valida se o cliente existe
        var client = await _userReadOnlyRepository.GetByIdentifier(request.ClientIdentifier)
            ?? throw new NotFoundException("ClientNotFound");

        request.UpdateEntity(billing);
        billing.Services = services.Select(s => s.ToServiceBillingEntity(billing.Id)).ToList();
        billing.TotalAmount = billing.Services.Sum(s => s.Price);

        _billingRepository.Update(billing);
        await _unitOfWork.Commit();
    }
}