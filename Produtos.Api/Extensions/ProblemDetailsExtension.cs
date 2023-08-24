using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;

namespace Produtos.Api.Extensions;

public static class ProblemDetailsExtension
{
    public static void AddApiProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (ctx, ex) => true;
            options.MapExceptionToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
            options.MapExceptionToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
            options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        })
        .AddProblemDetailsConventions();
    }

    public static void MapExceptionToStatusCode<TException>(this ProblemDetailsOptions options, int statusCode)
        where TException : Exception
    {
        options.Map<TException>(ex => new StatusCodeProblemDetails(statusCode)
        {
            Detail = ex.Message
        });
    }
}