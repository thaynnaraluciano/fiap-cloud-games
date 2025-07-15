using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CrossCutting.Exceptions.Middlewares
{
    public class MiddlewareTratamentoDeExcecoes
    {
        private readonly RequestDelegate _next;

        public MiddlewareTratamentoDeExcecoes(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ExcecaoNaoAutorizado ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
