using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Services;
using BarberBilling.Communication.Responses.Services;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Services;
using BarberBilling.Exceptions.CustomExceptions;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Services.Update;

public class UpdateServiceUseCase : IUpdateServiceUseCase
{
    private readonly IServiceUpdateOnlyRepository _updateRepository;
    private readonly IStringLocalizer<ResourceEnumResponse> _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServiceUseCase(
        IServiceUpdateOnlyRepository updateRepository,
        IStringLocalizer<ResourceEnumResponse> localizer,
        IUnitOfWork unitOfWork)
    {
        _updateRepository = updateRepository;
        _localizer = localizer;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseServiceJson> Execute(Guid serviceIdentifier, RequestServiceJson request)
    {
        new ServiceValidator().ValidateInput(request);

        var service = await _updateRepository.GetByIdentifier(serviceIdentifier)
            ?? throw new NotFoundException("ServiceNotFound");

        service.Services = (Domain.Enums.Services)request.Services;
        service.Price = request.Price;
        service.UpdatedAt = DateTime.UtcNow;

        await _updateRepository.Update(service);
        await _unitOfWork.Commit();

        return service.ToResponse(_localizer);
    }
}
