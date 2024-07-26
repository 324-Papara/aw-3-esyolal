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
        if (context.Request.Path.StartsWithSegments("/api/CustomerReport"))
        {
            await _next(context);
            return;
        }

        // Request Logging
        context.Request.EnableBuffering();
        var requestBodyStream = new MemoryStream();
        await context.Request.Body.CopyToAsync(requestBodyStream);
        requestBodyStream.Position = 0;
        var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
        _logger.LogInformation($"Request: Method: {context.Request.Method}, Path: {context.Request.Path}, QueryString: {context.Request.QueryString}, Body: {requestBodyText}");

        context.Request.Body.Position = 0;
        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            throw;
        }

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        _logger.LogInformation($"Response: StatusCode: {context.Response.StatusCode}, Body: {responseBodyText}");

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        await responseBodyStream.CopyToAsync(originalBodyStream);
    }
}
