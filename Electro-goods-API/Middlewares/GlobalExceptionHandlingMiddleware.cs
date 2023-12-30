using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Electro_goods_API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var lang = context.Request.Headers["Api-Lang"].ToString();
            if (string.IsNullOrEmpty(lang))
                lang = "ru";

            try
            {
                await next(context);
            }
            catch (HttpRequestException)
            {
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = lang == "ru" ? "Ошибка подключения к серверу." : "Помилка при підключенні до сервера.",
                    Title = lang == "ru" ? "Чтото пошло не так." : "Щось пішло не так.",
                    Detail = lang == "ru" ? "Проверьте правильность URL и повторите запрос." : "Перевірте правильність URL та повторіть запит.",
                };
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (ArgumentNullException)
            {
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = lang == "ru" ? "Ошибка в приложении клиента" : "Помилка у програмі клієнта",
                    Title = lang == "ru" ? "Не указаны необходимые данные в запросе" : "Не вказані необхідні дані у запиті",
                    Detail = lang == "ru" ? "Укажите все необходимые данные и повторите попытку" : "Вкажіть всі необхідні дані та повторіть спробу",
                };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (ArgumentOutOfRangeException e)
            {
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = lang == "ru" ? "Ошибка в приложении клиента" : "Помилка у програмі клієнта",
                    Title = lang == "ru" ? "Некорректные данные в запросе" : "Некоректні дані у запиті",
                    Detail = e.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (InvalidOperationException e)
            {
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = lang == "ru" ? "Ошибка при формировании ответа" : "Помилка для формування відповіді",
                    Title = lang == "ru" ? "Запрашиваемый ресурс не найден" : "Затребуваний ресурс не знайдено",
                    Detail = e.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (DbUpdateConcurrencyException e)
            {
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = lang == "ru" ? "Ошибка при формировании ответа" : "Помилка для формування відповіді",
                    Title = lang == "ru" ? "Не удалось получить данные из базы данных" : "Не вдалося отримати дані з бази даних",
                    Detail = e.Message,
                };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (Exception e)
            {

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = lang == "ru" ? "Ошибка сервева" : "Помилка для формування відповіді",
                    Title = lang == "ru" ? "Внутренняя ошибка сервера при обработке запроса" : "\r\nВнутрішня помилка сервера під час обробки запиту",
                    Detail = e.Message,
                };

                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
