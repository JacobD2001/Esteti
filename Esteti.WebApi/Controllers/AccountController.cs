using Azure.Core;
using Esteti.Application.Logic.Account;
using Esteti.Application.Logic.User;
using Esteti.Infrastructure.Auth;
using Esteti.WebApi.Application.Auth;
using Esteti.WebApi.Application.Response;
using MediatR;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Esteti.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(ILogger<AccountController> logger,
            IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetCurrentAccount()
        {
            var currentAccountDataResult = await _mediator.Send(new CurrentAccountQuery.Request() { });
            return Ok(currentAccountDataResult);
        }

    }
}
