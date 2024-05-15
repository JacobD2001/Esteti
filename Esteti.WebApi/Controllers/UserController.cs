using Azure.Core;
using Esteti.Application.Logic.User;
using Esteti.Infrastructure.Auth;
using Esteti.WebApi.Application.Auth;
using Esteti.WebApi.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Esteti.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly JwtManager _jwtManager;
        private readonly CookieSettings? _cookieSettings;
        private readonly IAntiforgery _antiforgery;
        public UserController(ILogger<UserController> logger,
            IOptions<CookieSettings> cookieSettings,
            JwtManager jwtManager,
            IAntiforgery antiforgery,
            IMediator mediator) : base(logger, mediator)
        {
            _jwtManager = jwtManager;
            _cookieSettings = cookieSettings != null ? cookieSettings.Value : null;
            _antiforgery = antiforgery;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task <ActionResult> CreateUserWithAccount([FromBody] CreateUserWithAccountCommand.Request model)
        {
            var createAccountResult = await _mediator.Send(model);
            var token = _jwtManager.GenerateUserToken(createAccountResult.UserId);
            SetTokenCookie(token);
            return Ok(new JwtToken() { AccessToken = token });
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginCommand.Request model)
        {
            var loginResult = await _mediator.Send(model);
            var token = _jwtManager.GenerateUserToken(loginResult.UserId);
            SetTokenCookie(token);
            return Ok(new JwtToken() { AccessToken = token });
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            var logoutResult = await _mediator.Send(new LogoutCommand.Request());
            DeleteTokenCookie();
            return Ok(logoutResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetLoggedInUser()
        {
            var loggedInUserResult = await _mediator.Send(new LoggedInUserQuery.Request() { });
            return Ok(loggedInUserResult);
        }

        [HttpGet]
        public async Task<ActionResult> AntiforgeryToken()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            return Ok(tokens.RequestToken);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.Lax
            };

            if (_cookieSettings != null)
            {
                cookieOption = new CookieOptions()
                {
                    HttpOnly = cookieOption.HttpOnly,
                    Expires = cookieOption.Expires,
                    Secure = _cookieSettings.Secure,
                    SameSite = _cookieSettings.SameSite
                };
            }

            Response.Cookies.Append(CookieSettings.CookieName, token, cookieOption);

        }

        private void DeleteTokenCookie()
        {
            Response.Cookies.Delete(CookieSettings.CookieName, new CookieOptions()
            {
                HttpOnly = true
            });
        }
    }
}
