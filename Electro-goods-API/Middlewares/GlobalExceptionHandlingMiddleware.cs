using Electro_goods_API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            context.Request.Headers.TryGetValue("Api-Language", out var lang);
            if (string.IsNullOrEmpty(lang))
                lang = "ru";

            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                ProblemDetails problem = new();

                switch (ex)
                {
                    case UserNotFoundException:
                        problem.Status = (int)HttpStatusCode.NotFound;
                        problem.Type = lang == "ru" ? "Ошибка" : "Помилка";
                        problem.Title = lang == "ru" ? "Не найден пользователь указанный в запросе" : "Не знайдено користувача вказаного у запиті";
                        problem.Detail = lang == "ru" ? "Измените параметры запроса и повторите попытку" : "Змініть параметри запиту та повторіть спробу";

                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsJsonAsync(problem);
                        break;


                    case NotFoundException:
                        problem.Status = (int)HttpStatusCode.NotFound;
                        problem.Type = lang == "ru" ? "Ошибка" : "Помилка";
                        problem.Title = lang == "ru" ? "Запрашиваемый ресурс не найден" : "Затребуваний ресурс не знайдено";
                        problem.Detail = lang == "ru" ? "Измените параметры запроса и повторите попытку" : "Змініть параметри запиту та повторіть спробу";

                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsJsonAsync(problem);
                        break;


                    case ArgumentNullException:
                        problem.Status = (int)HttpStatusCode.BadRequest;
                        problem.Type = lang == "ru" ? "Ошибка в приложении клиента" : "Помилка у програмі клієнта";
                        problem.Title = lang == "ru" ? "Не указаны необходимые данные в запросе к серверу" : "Не вказані необхідні дані у запиті до сервера";
                        problem.Detail = lang == "ru" ? "Укажите все необходимые данные и повторите попытку" : "Вкажіть всі необхідні дані та повторіть спробу";

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsJsonAsync(problem);
                        break;


                    case ArgumentOutOfRangeException:
                        problem.Status = (int)HttpStatusCode.BadRequest;
                        problem.Type = lang == "ru" ? "Ошибка в приложении клиента" : "Помилка у програмі клієнта";
                        problem.Title = lang == "ru" ? "Указаны неверные данные в запросе к серверу" : "Вказані невірні дані у запиті до сервера";
                        problem.Detail = lang == "ru" ? "Укажите все необходимые данные и повторите попытку" : "Вкажіть всі необхідні дані та повторіть спробу";

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsJsonAsync(problem);
                        break;


                    case ArgumentException:
                        problem.Status = (int)HttpStatusCode.BadRequest;
                        problem.Type = lang == "ru" ? "Ошибка в приложении клиента" : "Помилка у програмі клієнта";
                        problem.Title = lang == "ru" ? "Не указаны необходимые данные в запросе к серверу" : "Не вказані необхідні дані у запиті до сервера";
                        problem.Detail = lang == "ru" ? "Укажите все необходимые данные и повторите попытку" : "Вкажіть всі необхідні дані та повторіть спробу";

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsJsonAsync(problem);
                        break;


                    case DbUpdateConcurrencyException:
                        problem.Status = (int)HttpStatusCode.InternalServerError;
                        problem.Type = lang == "ru" ? "Ошибка сервера" : "Помилка сервера";
                        problem.Title = lang == "ru" ? "Запрос корректен, но произошла ошибка сервера при обработке запроса" : "Запит коректний, але помилка сервера при обробці запиту";
                        problem.Detail = lang == "ru" ? "Повторите попытку позже" : "Повторіть спробу пізніше";

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsJsonAsync(problem);
                        break;


                    default:
                        problem.Status = (int)HttpStatusCode.InternalServerError;
                        problem.Type = lang == "ru" ? "Ошибка сервера" : "Помилка сервера";
                        problem.Title = lang == "ru" ? "Внутренняя ошибка сервера при обработке запроса" : "Внутрішня помилка сервера під час обробки запиту";
                        problem.Detail = "Unhandled server error";
                        
                        _logger.LogCritical(ex, ex.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsJsonAsync(problem);
                        break;
                }

            }
        }
    }
}
