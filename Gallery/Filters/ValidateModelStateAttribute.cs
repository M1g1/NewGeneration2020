using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Gallery.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var isValid = actionContext.ModelState.IsValid;
            if (!isValid)
            {
                actionContext.Response.StatusCode = HttpStatusCode.BadRequest;
            }
            base.OnActionExecuting(actionContext);
        }

    }
}