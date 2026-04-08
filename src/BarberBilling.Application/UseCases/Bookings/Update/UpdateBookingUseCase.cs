using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Bookings;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Bookings.Update;

public class UpdateBookingUseCase : IUpdateBookingUseCase
{
    private readonly IBookingReadOnlyRepository _bookingReadRepository;
    private readonly IBookingUpdateOnlyRepository _bookingUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookingUseCase(
        IBookingUpdateOnlyRepository bookingUpdateRepository,
        IBookingReadOnlyRepository bookingReadRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingReadRepository = bookingReadRepository;
        _bookingUpdateRepository = bookingUpdateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id, BookingUpdateRequestJson request)
    {
        new BookingUpdateValidator().ValidateInput(request);

        var booking = await _bookingReadRepository.GetById(id)
            ?? throw new NotFoundException("BookingNotFound");

        // Só permite atualizar se estiver pendente
        if (booking.Status != Domain.Enums.BookingStatus.Pending)
            throw new ConflictException("BookingCannotBeUpdated");

        //booking.Status = request.Status;
        booking.Notes = request.Notes ?? booking.Notes;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingUpdateRepository.Update(booking);
        await _unitOfWork.Commit();
    }
}
