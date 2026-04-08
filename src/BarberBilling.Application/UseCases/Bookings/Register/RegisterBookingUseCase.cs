using BarberBilling.Application.Mappings;
using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Communication.Responses.Bookings.Register;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Bookings;
using BarberBilling.Domain.Repositories.Services;
using BarberBilling.Exceptions.CustomExceptions;
using ExpenseManagement.Exception;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Bookings.Register;

public class RegisterBookingUseCase : IRegisterBookingUseCase
{
    private readonly IBookingWriteOnlyRepository _bookingWriteOnlyRepository;
    private readonly IServiceReadOnlyRepository _serviceReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBookingUseCase(
        IBookingWriteOnlyRepository bookingWriteOnlyRepository,
        IServiceReadOnlyRepository serviceReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IStringLocalizer<ErrorMessages> localizer)
    {
        _bookingWriteOnlyRepository = bookingWriteOnlyRepository;
        _serviceReadOnlyRepository = serviceReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterBookingJson> Execute(BookingRequestJson request, Guid clientId)
    {
        new BookingValidator().ValidateInput(request);

        var entity = request.ToEntity(clientId);

        // Busca cada serviço e faz o snapshot
        foreach (var serviceId in request.ServiceIds)
        {
            var service = await _serviceReadOnlyRepository.GetByIdentifier(serviceId)
                ?? throw new NotFoundException("ServiceNotFound");

            entity.Services.Add(service.ToServiceBookingEntity(entity.Id));
        }

        // Calcula o total automaticamente
        entity.TotalAmount = entity.Services.Sum(s => s.Price);

        await _bookingWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();

        return entity.ToRegisterResponse();
    }
}
