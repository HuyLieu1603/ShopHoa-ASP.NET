using Newtonsoft.Json;
using ShopHoa.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopHoa.Utils
{
    public class Authorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session= filterContext.HttpContext.Session;
            string token = JwtHelper.GetTokenFromCookie();

            if (session["User"]==null && token!=null)
            {
                var user= JwtHelper.ValidateToken(token);

                var userJson = JsonConvert.SerializeObject(user,new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });

                if (user != null)
                {
                    session["User"]=user;
                }
            }
            if (string.IsNullOrEmpty(token) || JwtHelper.ValidateToken(token)==null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Auth/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}