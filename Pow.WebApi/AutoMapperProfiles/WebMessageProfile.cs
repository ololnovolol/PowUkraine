using AutoMapper;
using Pow.Application.Models;
using Pow.WebApi.Models;

namespace Pow.WebApi.AutoMapperProfiles
{
    public class WebMessageProfile : Profile
    {
        public WebMessageProfile()
        {
            CreateMap<MessageModel, MessageBL>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
