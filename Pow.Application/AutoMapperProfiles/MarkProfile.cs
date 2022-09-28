using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;

namespace Pow.Application.AutoMapperProfiles
{
    public class MarkProfile : Profile
    {
        public MarkProfile()
        {
            this.CreateMap<MarkBL, Mark>().ReverseMap();
        }
    }
}
