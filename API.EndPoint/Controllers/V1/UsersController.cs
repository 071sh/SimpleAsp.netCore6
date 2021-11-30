using Application.Dto;
using Application.Interface.Setting;
using Domain.Identity;
using Infrastructure.JwtService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.EndPoint.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;

        //public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _jwtService = jwtService;
        //}

        public UsersController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public virtual IActionResult login(string username, string password, string returnUrl = "/")
        //{
        //    var user = _userManager.FindByNameAsync(username).Result;
        //    if (user == null)
        //    {
        //        return NotFound(new BaseDto(false, new List<string> { "NotFount" }));
        //    }
        //    _signInManager.SignOutAsync();
        //    var result = _signInManager.PasswordSignInAsync(user, password
        //        , false, true).Result;

        //    if (result.Succeeded)
        //    {
        //        return Ok(returnUrl);
        //    }
        //    return BadRequest(new BaseDto(false, new List<string> { "Bad Request" }));
        //}


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
