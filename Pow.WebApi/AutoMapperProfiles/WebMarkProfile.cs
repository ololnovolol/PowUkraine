using System;
using AutoMapper;
using Pow.Application.Models;
using Pow.WebApi.Models;

namespace Pow.WebApi.AutoMapperProfiles
{
    public class WebMarkProfile : Profile
    {
        public WebMarkProfile()
        {
            this.CreateMap<MarkModel, MarkBL>()
                .ForMember(dest => dest.MessageId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
