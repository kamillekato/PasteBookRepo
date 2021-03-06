﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pastebook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        protected void Application_Error(object sender,EventArgs e)
        {
            var errorFound = Server.GetLastError() as HttpException;
            if (errorFound != null)
            {
                int errorCode = errorFound.ErrorCode;
                if (errorCode == 404)
                {
                    Response.Redirect("~/Error/Error404");
                }else
                {
                    Response.Redirect("~/Error/ErrorFound");
                }
            }else
            {
                Response.Redirect("~/Error/ErrorFound");
            }
        }
    }
}
