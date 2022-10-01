using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;

namespace Pow.Application.AutoMapperProfiles
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<AttachmentBL, Attachment>().ReverseMap();
        }
    }
}
