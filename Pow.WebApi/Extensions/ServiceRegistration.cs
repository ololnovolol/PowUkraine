using Microsoft.Extensions.DependencyInjection;
using Pow.Infrastructure.Repositories.Interfaces;
using Pow.Infrastructure.Repositories;

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
