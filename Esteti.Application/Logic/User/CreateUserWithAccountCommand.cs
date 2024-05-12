using Esteti.Application.Interfaces;
using Esteti.Application.Logic.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Application.Logic.User
{
    public static class CreateUserWithAccountCommand
    {
        public class  Request : IRequest<Result>
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
        public class Result
        {
            public required int UserId { get; set; }
        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
            private readonly IPasswordManager _passwordManager;

            public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext, IPasswordManager passwordManager) : base(currentAccountProvider, applicationDbContext)
            {
                _passwordManager = passwordManager;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var userExists = _applicationDbContext.Users.Any(u => u.Email == request.Email);
                if(userExists)
                    throw new Exception("AccountWithThisEmailAlreadyExists");

                var timeNow = DateTime.Now;
                var user = new Domain.Entities.User
                {
                    Email = request.Email,
                    HashedPassword = _passwordManager.HashPassword(request.Password), // or "" - here possible error(CHECK)
                    RegisterDate = timeNow
                };

                _applicationDbContext.Users.Add(user);

                var account = new Domain.Entities.Account()
                {
                    Name = request.Email,
                    CreatedAt = timeNow,
                };

                _applicationDbContext.Accounts.Add(account);

                var accountUser = new Domain.Entities.AccountUser()
                {
                    Account = account,
                    User = user,
                };

                _applicationDbContext.AccountUsers.Add(accountUser);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new Result
                {
                    UserId = user.Id
                };

            }
        }
    }
}
