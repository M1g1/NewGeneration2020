using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Service
{
    public class AuthenticationService : IAuthentication
    {
        public void AutorizeContext(IOwinContext ctx, ClaimsIdentity claims)
        {
            ctx.Authentication.SignOut();
            ctx.Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claims);
        }

    }
}
