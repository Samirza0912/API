using ApiPractice.Dtos.AccountDtos;
using ApiPractice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByNameAsync(registerDto.Username);
            if (user!=null)
            {
                return BadRequest("wrong");
            }
            user = new AppUser 
            {
                UserName = registerDto.Username,
                Fullname = registerDto.Fullname
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user==null)
            {
                return NotFound();
            }
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return NotFound();
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("id", user.Id));
            claims.Add(new Claim("Fullname", user.Fullname));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            string secretKey = "e0bc6b00-3180-40b0-9290-ca7fed8b8a66";
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //JwtSecurityToken jwt = new JwtSecurityToken(
            //    claims:claims,
            //    credentials:credentials,
            //    expires:DateTime.Now.AddDays(3)
            //    )
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = credentials,
                Audience = "http://localhost:5635/",
                Issuer = "http://localhost:5635/"
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDes);
            return Ok(new {token = tokenHandler.WriteToken(token) });
        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Admin"
            });
            result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Member"
            });
            return Ok();
        }
        [HttpGet("userprofile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            AppUser app = await _userManager.FindByNameAsync(User.Identity.Name);
            return Ok(new { name = app.UserName });
        }
    }
}
