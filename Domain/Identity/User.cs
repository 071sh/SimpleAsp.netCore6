using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Family { get; set; }
    }
}
