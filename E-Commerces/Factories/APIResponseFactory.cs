using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.ErrorModels;

namespace E_Commerces.Factories
{
    public static class APIResponseFactory
    {
        public static IActionResult GenerteApiValidationResponse(ActionContext Context)
        {

            var errorsInModelState = Context.ModelState
            .Where(e => e.Value.Errors.Any())
            .Select(e => new ValidationErrors()
            {
                Field = e.Key,
                Errors = e.Value.Errors.Select(er => er.ErrorMessage)
            }
            );
            var errorResponse = new ValidationErrorToReturn()
            {
                ValidationErrors = errorsInModelState
            };
            return new BadRequestObjectResult(errorResponse);

        }
        
    }
}
