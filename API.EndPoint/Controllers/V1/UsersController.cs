using API.EndPoint.ViewModel;
using Application.Dto;
using Application.Interface.Setting;
using Domain.Identity;
using Infrastructure.JwtService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.EndPoint.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public virtual IActionResult login(string username, string password, string returnUrl = "/")
        {
            var user =  _userManager.FindByNameAsync(username).Result;
            if (user != null && _userManager.CheckPasswordAsync(user, password).Result)
            {
                var userRoles =  _userManager.GetRolesAsync(user).Result;

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }


                return Ok(new BaseDto<string>(true,new List<string> { "Done"}, _jwtService.Execute(authClaims)));
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public virtual IActionResult Register([FromBody] UserModels.RegisterModel model)
        {
            var userExists =  _userManager.FindByNameAsync(model.Username).Result;
            if (userExists != null)
                return StatusCode(500, new BaseDto(false, new List<string>() { "User Already Exist" }));

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                Family=model.Family
            };
            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
                return StatusCode(500, new BaseDto(false, new List<string>() { "User creation failed! Please check user details and try again." }));

            return Ok(new BaseDto(true, new List<string> { "Done" }));
        }


        [HttpGet("CheckAuthorize")]
        [Authorize]
        public virtual IActionResult CheckAuthorize()
        {
            return Ok(new BaseDto(true, new List<string> { "Done" }));
        }
        [HttpGet("GetJwt")]
        public virtual IActionResult GetJwt()
        {
            var token= _jwtService.Execute(new List<System.Security.Claims.Claim> { new System.Security.Claims.Claim("Username", "shayanSafaei") });

            return Ok(new BaseDto<string>(true, new List<string> { "Done" },token));
        }
    }
}
