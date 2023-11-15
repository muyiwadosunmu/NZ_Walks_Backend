using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.DTOs;

namespace NZWalks_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }
        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterReqDto registerReqDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerReqDto.UserName,
                Email = registerReqDto.UserName
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerReqDto.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this user
                if (registerReqDto.Roles != null && registerReqDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerReqDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! , Please Login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto loginReqDto)
        {
            var user = await _userManager.FindByEmailAsync(loginReqDto.UserName);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginReqDto.Password);
                if (checkPasswordResult)
                {
                    // Create Token
                    return Ok("");
                }
            }
            return BadRequest("Username or password incorrect!!");
        }

    }
}