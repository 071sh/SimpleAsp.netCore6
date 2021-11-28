using AutoMapper;
using Domain.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MappingProfile.Setting
{
    public class AppSettingProfile:Profile
    {
        public AppSettingProfile()
        {
            CreateMap<AppSetting,Application.Dto.Setting.SettingDto.AppSettingDto>().ReverseMap();
        }
    }
}
