using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMR.CustomFilterAttribute
{
    public class CustomFilterAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {


            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.HttpContext.Session["Test"] == null)
            {
                throw new UnauthorizedAccessException("Authorization session is null");
            }
            else
            {
                bool result = false;
                //bool result = DBConnection.IsAuthorization(Convert.ToInt32(filterContext.HttpContext.Session["Test"]), filterContext.ActionDescriptor.ActionName.ToLower());
                if (!result)
                {
                    UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                    // filterContext.HttpContext.Response.Redirect(urlHelper.Action("error", "home"), true);
                    filterContext.Result = new RedirectResult(urlHelper.Action("error", "home"));
                }
                else
                {
                    //filterContext.Result = new ViewResult();
                }

            }
            base.OnAuthorization(filterContext);
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    //if (!httpContext.Request.IsAuthenticated)
        //    //    return false;
        //    string url = HttpContext.Current.Request.RawUrl;
        //    System.Web.Routing.RouteData route = System.Web.Routing.RouteTable.Routes.GetRouteData(httpContext);
        //    //UrlHelper urlHelper = new UrlHelper(new RequestContext(httpContext, route));

        //    //var routeValueDictionary = urlHelper.RequestContext.RouteData.Values;
        //    //string controllerName = routeValueDictionary["controller"].ToString();
        //    //string actionName = routeValueDictionary["action"].ToString();

        //    var rd = httpContext.Request.RequestContext.RouteData;
        //    string currentAction = rd.GetRequiredString("action");
        //    string currentController = rd.GetRequiredString("controller");
        //    string currentArea = rd.Values["area"] as string;
        //    if (!DBConnection.IsAuthorization(Convert.ToInt32(HttpContext.Current.Session["Test"]), currentAction.ToLower())) // implement this method based on your tables and logic
        //    {

        //        return false;

        //    }
        //    else
        //        return true;



        //}
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.Result = new HttpStatusCodeResult(403, "Sorry, you do not have the required permission to perform this action.");

            }
            else
            {
                var viewResult = new ViewResult();
                viewResult.ViewName = "~/Views/Home/error.cshtml";
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = viewResult;
            }

            //   base.HandleUnauthorizedRequest(filterContext);
        }
    }
}