using BarberBilling.Communication.Responses.Categories;

namespace BarberBilling.Application.UseCases.Categories.GetAll;

public interface IGetAllCategoriesUseCase
{
    Task<ResponseCategoriesJson> Execute();
}