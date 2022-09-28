using System;
using System.Reflection;
using AutoMapper;
using Pow.Application.Models;
using Pow.Application.Services;
using Pow.WebApi.Models;

namespace Pow.WebApi.AutoMapperProfiles
{
    public class WebAttachmentProfile : Profile
    {
        public WebAttachmentProfile()
        {
            this.CreateMap<AttachmentModel, AttachmentBL>()
                .ForMember(dest => dest.MessageId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
