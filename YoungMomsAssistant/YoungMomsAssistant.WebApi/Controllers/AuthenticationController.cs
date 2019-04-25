using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Domain.Users;
using YoungMomsAssistant.Core.Domain.Users.JWT;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.WebApi.Constants;

namespace YoungMomsAssistant.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase {

        private IUserManager _userManager;

        public AuthenticationController(IUserManager userManager) {
            _userManager = userManager;
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> SingInAsync(
            [FromBody] UserDto userDto,
            [FromServices] IJwtSigningEncodingKey jwtSigningEncodingKey) {
            var user = await _userManager.AuthUserAsync(userDto);

            if (user == null) {
                return BadRequest();
            }

            var claims = new Claim[] {
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var token = new JwtSecurityToken(
                issuer: AuthConstants.Issuer,
                audience: AuthConstants.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(AuthConstants.LifeTimeMins),
                signingCredentials: new SigningCredentials(
                    jwtSigningEncodingKey.Key,
                    jwtSigningEncodingKey.SigningAlgorithm)
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("SignOut")]
        [AllowAnonymous]
        public async Task<ActionResult> SignOutAsync([FromBody] UserDto userDto) {
            var result = await _userManager.RegisterAsync(userDto);

            if (result) {
                return Ok();
            }
            else {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        public string[] Get() {
            return new string[] { "Auth", "Hura" };
        }
    }
}