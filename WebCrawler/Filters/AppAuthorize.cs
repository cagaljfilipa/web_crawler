using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.AppHelpers;

namespace WebCrawler.Filters
{
    public class AppAuthorize : ActionFilterAttribute
    {

        public string CurrentPagePermission { get; set; }
        public AppAuthorize()
        {

        }

        public AppAuthorize(string permission)
        {
            CurrentPagePermission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            //if user is not logged in, return to login page
            var session = context.HttpContext.Session.GetSession();
            if (session == null)
            {
                context.Result = new RedirectResult(string.Format("/Account/Login/"));
                base.OnActionExecuting(context);

            }

            if(session!=null && session.User.UserRoleId != 1)
            {
                context.Result = new RedirectResult(string.Format("/Scrapper/Index/"));
                base.OnActionExecuting(context);

            }



        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
