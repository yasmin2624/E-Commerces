using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Attributes
{
    public class Cashe(int DurationInSec = 90):ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string CasheKey = CreateCasheKey(context.HttpContext.Request);
            ICashService cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();

            var CashValue = await cashService.GetAsync(CasheKey);

            if (CashValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CashValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return; 
            }

           
            var executedContext = await next.Invoke();

            if (executedContext.Result is OkObjectResult result)
            {
                var json = System.Text.Json.JsonSerializer.Serialize(result.Value);
                await cashService.SetAsync(CasheKey, json, TimeSpan.FromSeconds(DurationInSec));
            }
        }

        private string CreateCasheKey(HttpRequest request)
        {
            // baseurl/api/products?typeid=10?brandId=
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path + '?');

            foreach (var Item in request.Query.OrderBy(Q => Q.Key))
            {
                Key.Append($"{Item.Key}={Item.Value}&");
            }

            return Key.ToString();
        }
    }
}
