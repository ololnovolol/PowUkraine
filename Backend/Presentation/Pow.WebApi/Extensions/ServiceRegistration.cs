using Microsoft.Extensions.DependencyInjection;
using Pow.Persistance.Repositories.Interfaces;
using Pow.Persistance.Repositories;

namespace Pow.WebApi.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMarkRepository, MarkRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
