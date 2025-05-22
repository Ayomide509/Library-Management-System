namespace LibraryManagementSystem.API.MiddleWare
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                var errorResponse = new
                {
                    status = context.Response.StatusCode,
                    message = ex.Message switch
                    {
                        _ when ex is UnauthorizedAccessException => "401 Unauthorized: Access denied.",
                        _ when ex is KeyNotFoundException => "404 Not Found: Resource not found.",
                        _ when ex is ArgumentException => "400 Bad Request: Invalid input provided.",
                        _ => "500 Internal Server Error: Something went wrong."
                    }
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }

}
