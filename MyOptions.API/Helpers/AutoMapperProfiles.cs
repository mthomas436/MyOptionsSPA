using AutoMapper;
using MyOptions.API.Dtos;
using MyOptions.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOptions.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<UserForListDto, User>()
                .ForMember(dest => dest.Userid, opt => opt.Ignore());
            CreateMap<UserForRegisterDto, User>()
                 .ForMember(dest => dest.Userid, opt => opt.Ignore())
                 .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                 .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            CreateMap<TradeForInsertDto, Trade>()
                .ForMember(dest => dest.Tradeid, opt => opt.Ignore())
                .ForMember(dest => dest.DateClosed, opt => opt.Ignore())
                .ForMember(dest => dest.TradeTypes, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore());

            CreateMap<OptionForUpdateDto, Option>()
                .ForMember(dest => dest.DateEntered, opt => opt.Ignore())
                .ForMember(dest => dest.Trades, opt => opt.Ignore())
                .ForMember(dest => dest.Tradeid, opt => opt.Ignore())
                .ForMember(dest => dest.OptionType, opt => opt.Ignore());
        }
    }
}
