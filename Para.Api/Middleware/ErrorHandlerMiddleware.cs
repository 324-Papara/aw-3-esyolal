using System.Text.Json;

namespace Para.Api.Middleware;


public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // before controller invoke
            await next.Invoke(context);
            // after controller invoke
        }
        catch (Exception ex)
        {
            // log


            var errorResponse = new
            {
                Message = "Internal Server Error",
                Detail = ex.Message
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);

            context.Response.StatusCode = 500;
            context.Request.ContentType = "application/json";
            await context.Response.WriteAsync(errorJson);
        }

    }

}