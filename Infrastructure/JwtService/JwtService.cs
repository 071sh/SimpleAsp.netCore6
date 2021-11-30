using Application.Interface.Setting;
using Domain.Setting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JwtService
{
    public class JwtService : IJwtService
    {
        private readonly IAppSettingService _appSettingService;

        public JwtService(IAppSettingService appSettingService)
        {
            _appSettingService = appSettingService;
        }

        public string Execute(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettingService.GetValue("Jwt.SecretKey")));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _appSettingService.GetValue("Jwt.Issuer"),
                audience: _appSettingService.GetValue("Jwt.Audience"),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_appSettingService.GetValue("Jwt.Expires"))),
                notBefore: DateTime.Now.AddMinutes(Convert.ToDouble(_appSettingService.GetValue("Jwt.NotBefore"))),
                claims: claims,
                signingCredentials: credentials);
               

            var jwtToken=new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;

        }
    }
}
