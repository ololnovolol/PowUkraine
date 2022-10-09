using AutoMapper;
using Pow.Application.Models;
using Pow.WebApi.Models;

namespace Pow.WebApi.AutoMapperProfiles
{
    public class WebMessageMarkProfile : Profile
    {
        public WebMessageMarkProfile()
            => CreateMap<MessageWithMarkModel, MessageMarkBL>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                /*.ForMember(dest => dest.EventDate)*/
                .ReverseMap();
    }
}
