using BarberBilling.Communication.Responses.Services;
using BarberBilling.Domain.Entities.Filters;

namespace BarberBilling.Application.UseCases.Services.GetAll;

public interface IGetAllServicesUseCase
{
    Task<ResponseServicesJson> Execute(ServiceFilter filter);
}