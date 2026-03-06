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

            // header validation
            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(userId))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("UserId and Role headers are required");
                return;
            }

            // Hardcoded users
            if (userId == "1" && role == "Admin")
            {
                context.Items["UserId"] = userId;
                context.Items["Role"] = role;
            }
            else if (userId == "2" && role == "User")
            {
                context.Items["UserId"] = userId;
                context.Items["Role"] = role;
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid user credentials");
                return;
            }

            await _next(context);
        }
    }
}