using AutoMapper;
using Pow.Application.Models;
using Pow.WebApi.Models;

namespace Pow.WebApi.AutoMapperProfiles
{
    public class WebMessageMarkProfile : Profile
    {
        public WebMessageMarkProfile()
            => CreateMap<MessageWithMarkModel, MessageMarkBl>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
    }
}
