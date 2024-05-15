using Esteti.Application.Exceptions;
using Esteti.Application.Interfaces;
using Esteti.Application.Logic.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Application.Logic.User
{
    public static class LogoutCommand
    {
        public class Request : IRequest<Result>
        {

        }

        public class Result
        {

        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {

            public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Result()
                {
                    //not implemented - maybe for adding logs that user logged out
                };
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }
    }
}
