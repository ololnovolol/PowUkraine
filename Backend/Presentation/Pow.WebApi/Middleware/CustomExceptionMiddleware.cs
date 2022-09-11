using FluentValidation;
using Microsoft.AspNetCore.Http;
using Pow.Application.Common.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                {
                    ValidationException validation = validationException;
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validation.Errors);

                    break;
                }
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }


        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == String.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}

