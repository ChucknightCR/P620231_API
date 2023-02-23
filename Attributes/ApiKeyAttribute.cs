using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Validations;

namespace P620231_API.Attributes
{
    [AttributeUsage(validOn:AttributeTargets.All)]

    public sealed class ApiKeyAttribute : Attribute , IAsyncActionFilter
    {
        //Escribimos la funcionalidad para la decoración "ApiKey", que luego usaremos en los controles
        //Para limitar y dar seguridad la forma de consumiremos un end point



        private readonly string _apiKey = "P6ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key no Proporcionada!"
                };
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

            if(!ApiKeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "La llave de seguridad suministrada no es correcta"
                };
                return;
            }

            await next();


        }

    }
   
}
