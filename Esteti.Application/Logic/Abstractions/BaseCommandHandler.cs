using Esteti.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Application.Logic.Abstractions
{
    public abstract class BaseCommandHandler
    {
        private readonly ICurrentAccountProvider _currentAccountProvider;
        private readonly IAuthenticationDataProvider _authenticationDataProvider;

        protected BaseCommandHandler(ICurrentAccountProvider currentAccountProvider, IAuthenticationDataProvider authenticationDataProvider)
        {
            _currentAccountProvider = currentAccountProvider;
            _authenticationDataProvider = authenticationDataProvider;
        }
    }
}
