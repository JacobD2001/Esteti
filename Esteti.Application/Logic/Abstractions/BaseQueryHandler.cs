using Esteti.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Application.Logic.Abstractions
{
    public abstract class BaseQueryHandler
    {
        protected readonly ICurrentAccountProvider _currentAccountProvider;
        protected readonly IAuthenticationDataProvider _authenticationDataProvider;

        protected BaseQueryHandler(ICurrentAccountProvider currentAccountProvider, IAuthenticationDataProvider authenticationDataProvider)
        {
            _currentAccountProvider = currentAccountProvider;
            _authenticationDataProvider = authenticationDataProvider;
        }

    }
}
