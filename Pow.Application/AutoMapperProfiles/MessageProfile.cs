using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;

namespace Pow.Application.AutoMapperProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile() { 
         this.CreateMap<MessageBL, Message>().ReverseMap();
        }
    }
}
