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
            /* no mapping like this, as itll cause unnecessary queries, add a field for this in message model and use const names
            CreateMap<Message, MessageOutDto>()
                .ForMember(dest => dest.SenderUserName, act => act.MapFrom(src => src.Sender.UserName))
                .ForMember(dest => dest.ReceiverUserName, act => act.MapFrom(src => src.Receiver.UserName));
            */
            CreateMap<Message, MessageOutDto>();
        }
    }
}
