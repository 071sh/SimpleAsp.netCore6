using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Setting
{
    public class SettingDto
    {
        public class AppSettingDto
        {
            public int Id { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
            public DateTime? DateTime { get; set; }
            public string Description { get; set; }
        }
    }
}
