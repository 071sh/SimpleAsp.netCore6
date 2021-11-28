using Application.Dto;
using Application.Dto.Setting;
using Application.Interface.Setting;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Setting
{
    public class AppSettingService : IAppSettingService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AppSettingService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseDto<SettingDto.AppSettingDto> Excecute(string key)
        {
            var data = _context.AppSettings.FirstOrDefault(x => x.Key == key);
            if (data == null)
                return null;

            var result= _mapper.Map<SettingDto.AppSettingDto>(data);

            return new BaseDto<SettingDto.AppSettingDto>(true, new List<string> { "Done" }, result);
        }
    }
}
