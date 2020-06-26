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

            var queueParser = container.Resolve<IQueueParser>();
            var queueInit = container.Resolve<IQueueInitialize>();
            var queueNames = queueParser.ParseQueueNames();
            foreach (var name in queueNames)
            {
                queueInit.CreateIfNotExist(name);
            }
        }
    }
}