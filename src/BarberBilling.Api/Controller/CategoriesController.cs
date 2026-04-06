using BarberBilling.Api.Security.Authorization;
using BarberBilling.Application.UseCases.Categories.GetAll;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[Route("api/v1/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = Permissions.Categories.Read)]
    [ProducesResponseType(typeof(List<ResponseCategoryJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllCategoriesUseCase useCase
    )
    {
        var response = await useCase.Execute();
        return Ok(response);
    }
}