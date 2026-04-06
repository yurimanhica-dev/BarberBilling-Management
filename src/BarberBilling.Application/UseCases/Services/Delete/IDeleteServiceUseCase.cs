using BarberBilling.Communication.Responses.Services.Delete;

namespace BarberBilling.Application.UseCases.Services.Delete;

public interface IDeleteServiceUseCase
{
    Task<ResponseSoftDeleteJson> Execute(Guid serviceIdentifier, bool deleted);
}