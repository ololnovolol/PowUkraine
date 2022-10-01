using Microsoft.Extensions.DependencyInjection;
using Pow.Infrastructure.Repositories;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.WebApi.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IMessageRepository, MessageRepository>()
                .AddTransient<IMarkRepository, MarkRepository>()
                .AddTransient<IAttachmentRepository, AttachmentRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
