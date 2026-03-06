namespace TaskManagement.API.Middleware
{
    public class FakeAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public FakeAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var role = context.Request.Headers["Role"].ToString();
            var userId = context.Request.Headers["UserId"].ToString();

            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(userId))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("UserId and Role headers are required");
                return;
            }

            context.Items["UserId"] = userId;
            context.Items["Role"] = role;

            await _next(context);
        }
    }
}