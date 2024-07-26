using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

public class ReqResLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ReqResLoggingMiddleware> _logger;

    public ReqResLoggingMiddleware(RequestDelegate next, ILogger<ReqResLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
       
        var request = await FormatRequest(context.Request);
        _logger.LogInformation($"Request: {request}");

        
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;
            await _next(context);
            var response = await FormatResponse(context.Response);
            _logger.LogInformation($"Response: {response}");
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        using (var reader = new StreamReader(request.Body, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; 
            return $"Method: {request.Method}, Path: {request.Path}, QueryString: {request.QueryString}, Body: {body}";
        }
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin); 
        return $"StatusCode: {response.StatusCode}, Body: {body}";
    }
}
