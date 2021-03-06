using Knab.CodingAssessment.Exchange.Endpoint.WebApi.Controllers;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using Knab.CodingAssessment.Exchange.Common;

namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ApiConfiguration apiConfiguration, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _apiConfiguration = apiConfiguration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.ToString());

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            await context.Response.WriteAsync(JsonSerializer.Serialize(ApiResult.Error(ExceptionMessages.UnhandledException)));
        }
    }
}
