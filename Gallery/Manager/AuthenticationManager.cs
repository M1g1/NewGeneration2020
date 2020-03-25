using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace Gallery
{
    public class AuthenticationManager : IAuthentication
    {
        public void AutorizeContext(IOwinContext ctx, ClaimsIdentity claims)
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
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, value, ClaimValueTypes.String));
            claims.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));
            return  claims;
        }
    }
}
