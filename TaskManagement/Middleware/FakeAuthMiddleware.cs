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

            context.Items["Role"] = role;
            context.Items["UserId"] = userId;

            await _next(context);
        }
    }
}
