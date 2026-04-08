namespace BarberBilling.Application.UseCases.Bookings.Delete;

public interface IDeleteBookingUseCase
{
    Task Execute(Guid id);
}
