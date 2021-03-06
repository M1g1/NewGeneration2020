﻿using System.Web.Mvc;
using NLog;

namespace Gallery.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            var machineName = filterContext.HttpContext.Server.MachineName;
            var dnsName = request.UserHostName;
            var rawUrl = request.RawUrl;
            var userAgent = request.UserAgent;
            var httpMethod = request.HttpMethod;
            string messageRequest = "\n{\n\tRequest\n\tIP: " + ipAddress + "\n\tDNS-name: " + dnsName + "\n\tMachineName: " +
                                    machineName + "\n\tURL-address: " + rawUrl + "\n\tUserAgent: " + userAgent +
                                    "\n\tHttpMethod: " + httpMethod + "\n}";
            Logger.Info(messageRequest);
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            var status = response.Status;
            string messageResponse = "\n{\n\tResponse" + "\n\tStatusCode: " + status + "\n}";
            Logger.Info(messageResponse);
            base.OnActionExecuted(filterContext);
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;
            }
            var exception = filterContext.Exception;
            Logger.Error(exception);
        }
    }
}