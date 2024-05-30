namespace Electro_goods_API.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var language = context.Request.Headers["Api-Language"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(language))
                language = "ru"; // Default language}

            context.Items["Language"] = language;

            await _next(context);
        }
    }

}
