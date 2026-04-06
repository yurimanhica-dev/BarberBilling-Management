using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Responses.Categories;
using BarberBilling.Domain.Repositories.Categories;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Categories.GetAll;

public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
{
    private readonly IStringLocalizer<ResourceEnumResponse> _localizer;
    private readonly ICategoryReadOnlyRepository _repository;
    public GetAllCategoriesUseCase(ICategoryReadOnlyRepository repository, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }
    public async Task<ResponseCategoriesJson> Execute()
    {
        var categories = await _repository.GetAllAsync();

        return new ResponseCategoriesJson
        {
            Categories = categories.ToResponseCategory(_localizer)
        };
    }
}