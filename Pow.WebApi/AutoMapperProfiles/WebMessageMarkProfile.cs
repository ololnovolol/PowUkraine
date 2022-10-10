using AutoMapper;
using Pow.Application.Models;
using Pow.WebApi.Models;

namespace Pow.WebApi.AutoMapperProfiles
{
    public class WebMessageMarkProfile : Profile
    {
        public WebMessageMarkProfile()
            => CreateMap<MessageWithMarkModel, MessageMarkBL>()
                .ReverseMap();
    }
}
