using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Bookings;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Bookings.Delete;

public class DeleteBookingUseCase : IDeleteBookingUseCase
{
    private readonly IBookingWriteOnlyRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookingUseCase(
        IBookingWriteOnlyRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id)
    {
        var deleted = await _bookingRepository.Delete(id);
        if (!deleted)
            throw new NotFoundException("BookingNotFound");

        await _unitOfWork.Commit();
    }
}
