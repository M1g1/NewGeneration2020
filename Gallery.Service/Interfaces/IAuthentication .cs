using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Gallery.Service
{
    public interface IAuthentication
    {
        void AutorizeContext(IOwinContext ctx, ClaimsIdentity claims);


    }
}
