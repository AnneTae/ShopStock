using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoopStock.Api.Models.Errors;
using ShoopStock.Core.Exceptions;
using ShoopStock.Core.Identity;

namespace ShoopStock.Api.Controllers;

[ApiController]
[Produces("application/json")]
public class BaseApiController : Controller
{
    protected static ErrorResponseModel ExceptionFilter(Exception exception)
    {
        if (exception.GetType() == typeof(ValidationException))
        {
            var validationException = (ValidationException)exception;
            var massages = validationException.Errors.Select(failure => failure.ErrorMessage).Distinct().ToList();

            return new ErrorResponseModel(massages);
        }

        if (exception.GetType() == typeof(InformationException))
        {
            var identityException = (InformationException)exception;
            return new ErrorResponseModel(identityException.Errors);
        }

        return exception switch
        {
            ValidationException _ => new ErrorResponseModel(exception.Message),
            EntityNotFoundException _ => new ErrorResponseModel(exception.Message),
            EntityAlreadyExistException _ => new ErrorResponseModel(exception.Message),
            UnauthorizedException _ => new ErrorResponseModel(exception.Message),
            InvalidAssignException _ => new ErrorResponseModel(exception.Message),
            FormatException _ => new ErrorResponseModel(exception.InnerException!.Message),
            UnconfirmedException _ => new ErrorResponseModel(exception.Message),
            ArgumentNullException _ => new ErrorResponseModel(exception.Message),
            PaymentExceptions _ => new ErrorResponseModel(exception.Message),
            CustomException _ => new ErrorResponseModel(exception.Message),
            InformationException => new ErrorResponseModel(),
            _ => new ErrorResponseModel(exception.Message)
        };
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        if (!filterContext.ModelState.IsValid)
        {
            var errorInModelState = filterContext.ModelState.Where(x => x.Value!.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            var errorResponseModel = new ErrorResponseModel(errorInModelState);
            var errorResponse = new ObjectResult(errorResponseModel)
            {
                StatusCode = 400
            };

            filterContext.Result = errorResponse;
        }
    }
}
