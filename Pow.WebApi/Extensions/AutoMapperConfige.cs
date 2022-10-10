using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Pow.Application.AutoMapperProfiles;
using Pow.WebApi.AutoMapperProfiles;

namespace Pow.WebApi.Extensions
{
    public static class AutoMapperConfige
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DateTime, string>().ConvertUsing(new DataToStringConverter());
                cfg.AddProfile<WebAttachmentProfile>();
                cfg.AddProfile<WebMarkProfile>();
                cfg.AddProfile<WebMessageProfile>();
                cfg.AddProfile<MarkProfile>();
                cfg.AddProfile<MessageProfile>();
                cfg.AddProfile<AttachmentProfile>();
                cfg.AddProfile<WebMessageMarkProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
