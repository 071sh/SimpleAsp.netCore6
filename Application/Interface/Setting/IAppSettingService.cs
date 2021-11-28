using Application.Dto;
using Application.Dto.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Setting
{
    public interface IAppSettingService
    {
        BaseDto<SettingDto.AppSettingDto> Excecute(string key);
    }
}
