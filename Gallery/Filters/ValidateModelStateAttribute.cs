using System.Web.Mvc;

namespace Gallery.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isValid = filterContext.Controller.ViewData.ModelState.IsValid;
            if (!isValid)
            {
                filterContext.HttpContext.Response.StatusCode = 400;
            }
            base.OnActionExecuting(filterContext);
        }

    }
}