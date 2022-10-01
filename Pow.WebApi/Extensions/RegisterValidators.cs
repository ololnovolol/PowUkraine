using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pow.Domain;
using Pow.Domain.Validators;

namespace Pow.WebApi.Extensions
{
    public static class RegisterValidators
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Attachment>, AttachmentValidator>()
                .AddScoped<IValidator<Message>, MessageValidator>()
                .AddScoped<IValidator<Mark>, MarkValidator>();
        }
    }
}
