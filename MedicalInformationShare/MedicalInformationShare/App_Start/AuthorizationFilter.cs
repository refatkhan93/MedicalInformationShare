using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalInformationShare.App_Start
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }
            
            // Check for authorization  
            if (HttpContext.Current.Session["HospitalId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/Login");
                /*filterContext.Result = new HttpUnauthorizedResult();*/
            }
            /*else
            {
                filterContext.Result = new RedirectResult("~/Primary/Index");
            }*/
            
        }
    } 
}