using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Domain.Users;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.WebApi.Constants;
using YoungMomsAssistant.WebApi.Models.DtoModels;
using YoungMomsAssistant.WebApi.Services.JWT;

namespace YoungMomsAssistant.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase {

        private IUserManager _userManager;
        private IRefreshTokensCollection _refreshTokensCollection;
        public AuthenticationController(
            IUserManager userManager, 
            IRefreshTokensCollection refreshTokensCollection) {
            _userManager = userManager;
            _refreshTokensCollection = refreshTokensCollection;
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> SingInAsync(
            [FromBody] UserDto userDto,
            [FromServices] IJwtService jwtService) {
            var user = await _userManager.AuthUserAsync(userDto);

            if (user == null) {
                return BadRequest();
            }

            return Ok(new TokensDto {
                Token = jwtService.GenerateToken(user),
                RefreshToken = _refreshTokensCollection.Generate(user.Email)
            });
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

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public ActionResult RefreshToken(
            [FromBody] TokensDto tokensDto, 
            [FromServices] IJwtService jwtService) {

            var principal = jwtService.GetPrincipalFromExpiredToken(tokensDto.Token);

            var email = principal.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            var savedRefreshToken = _refreshTokensCollection.Get(email);

            if (savedRefreshToken != tokensDto.RefreshToken) {
                throw new SecurityTokenException("refreshToken");
            }

            return Ok(new TokensDto {
                Token = jwtService.GenerateToken(new UserDto { Email = email}),
                RefreshToken = _refreshTokensCollection.Generate(email)
            });
        }
    }
}