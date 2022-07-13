using AutoMapper;
using Model.Entities;
using Service.DtoModel.DtoIn;
using Service.DtoModel.DtoOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingsProfiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUserInDto, ApplicationUser>()
                .ForPath(dst => dst.Details.Race, act => act.MapFrom(src => src.Race))
                .ForPath(dst => dst.Details.Occupation, act => act.MapFrom(src => src.Occupation))
                .ForPath(dst => dst.Details.Experience, act => act.MapFrom(src => src.Experience))
                .ForPath(dst => dst.Details.Home, act => act.MapFrom(src => src.Home))
                .ForPath(dst => dst.Details.HasEquipment, act => act.MapFrom(src => src.hasEquipment))
                .ForPath(dst => dst.Details.Likes, act => act.MapFrom(src => src.Likes))
                .ForPath(dst => dst.Details.Dislikes, act => act.MapFrom(src => src.Dislikes))
                .ForPath(dst => dst.Details.Specialty, act => act.MapFrom(src => src.Specialty))
                .ForPath(dst => dst.Details.AboutMe, act => act.MapFrom(src => src.AboutMe))
                .ForAllMembers(x => x.AllowNull());
            //unfortunately we cant just do ForAllMembers because of userName...
            CreateMap<ApplicationUser, ApplicationUserOutDto>()
                .ForMember(x => x.Race, a => a.MapFrom(s => s.Details.Race))
                .ForMember(x => x.Occupation, a => a.MapFrom(s => s.Details.Occupation))
                .ForMember(x => x.Experience, a => a.MapFrom(s => s.Details.Experience))
                .ForMember(x => x.Home, a => a.MapFrom(s => s.Details.Home))
                .ForMember(x => x.HasEquipment, a => a.MapFrom(s => s.Details.HasEquipment))
                .ForMember(x => x.Likes, a => a.MapFrom(s => s.Details.Likes))
                .ForMember(x => x.Dislikes, a => a.MapFrom(s => s.Details.Dislikes))
                .ForMember(x => x.Specialty, a => a.MapFrom(s => s.Details.Specialty))
                .ForMember(x => x.AboutMe, a => a.MapFrom(s => s.Details.AboutMe));
        }
    }
}
