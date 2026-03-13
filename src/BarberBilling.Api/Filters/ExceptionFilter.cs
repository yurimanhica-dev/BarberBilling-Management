using ExpenseManagement.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BarberBilling.Exceptions.Base;
using Microsoft.Extensions.Localization;
using BarberBilling.Communication.Responses;

namespace ExpenseManagement.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    public ExceptionFilter(IStringLocalizer<ErrorMessages> localizer)
    {
        _localizer = localizer;
    }
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberBillingException)
            HandleProcessException(context);
        else
            ThrowUnknownError(context);
    }

    private void HandleProcessException(ExceptionContext context)
    {
        var expenseException = (BarberBillingException)context.Exception;

        var localizedErrors = expenseException
            .GetErrors()
            .Select(key => _localizer[key].Value)
            .ToList();

        var errorResponse = new ResponseErrorJson(localizedErrors);

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = expenseException.StatusCode
        };
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var message = _localizer["UnexpectedError"].Value;

        var errorResponse = new ResponseErrorJson(message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}