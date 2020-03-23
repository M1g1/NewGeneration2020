using Microsoft.Owin;
using System.Security.Claims;

namespace Gallery
{
    public interface IAuthentication
    {
        void AutorizeContext(IOwinContext ctx, ClaimsIdentity claims);
        ClaimsIdentity CreateClaimsIdentity(string userId);

    }
}
