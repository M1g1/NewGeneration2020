using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace Gallery.Manager
{
    public class AuthenticationManager : IAuthentication
    {
        public void LogIn(IOwinContext ctx, ClaimsIdentity claims)
        {
            ctx.Authentication.SignOut();
            ctx.Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claims);
        }

        public ClaimsIdentity CreateClaimsIdentity(string value)
        {
            ClaimsIdentity claims = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claims.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, value, ClaimValueTypes.String));
            return  claims;
        }

        public void LogOut(IOwinContext ctx)
        {
            ctx.Authentication.SignOut();
        }
    }
}
