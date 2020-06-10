using Microsoft.Owin;
using System.Security.Claims;

namespace Gallery
{
    public interface IAuthentication
    {
        void LogIn(IOwinContext ctx, ClaimsIdentity claims);
        ClaimsIdentity CreateClaimsIdentity(string userId);
        void LogOut(IOwinContext ctx);
    }
}
