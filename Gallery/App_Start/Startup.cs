using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Web.Http;
using Autofac;
using Gallery.MessageQueues;

[assembly: OwinStartup(typeof(Gallery.App_Start.Startup))]
namespace Gallery.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
                CookieName = "LoginCookie",
                ExpireTimeSpan = TimeSpan.FromDays(30),
                SlidingExpiration = true
            });
            var container = DIConfig.Configure(new HttpConfiguration());


            var parsedDictionary = Parser.ParseQueueNames();
          
            var queueInit = container.Resolve<IQueueInitialize>();
            
            foreach (var kvp in parsedDictionary)
            {
                queueInit.CreateIfNotExist(kvp.Value);
            }
        }
    }
}