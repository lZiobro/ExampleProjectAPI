using AutoMapper;
using Model.Entities;
using Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingsProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageInDto, Message>();
            CreateMap<Message, MessageOutDto>();
        }
    }
}
