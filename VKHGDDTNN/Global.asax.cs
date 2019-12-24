using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace VKHGDDTNN
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Application["PageView"] = 0;
            //Application["Online"] = 0;
            if (!File.Exists(Server.MapPath("Count_Visited.txt")))
                File.WriteAllText(Server.MapPath("Count_Visited.txt"), "0");
            Application["DaTruyCap"] = int.Parse(File.ReadAllText(Server.MapPath("Count_Visited.txt")));
        }

        protected void Session_Start()
        {
            Application.Lock();
            if (Application["DangTruyCap"] == null)
                Application["DangTruyCap"] = 1;
            else
                Application["DangTruyCap"] = (int)Application["DangTruyCap"] + 1;
            // Tăng số đã truy cập lên 1 nếu có khách truy cập
            Application["DaTruyCap"] = (int)Application["DaTruyCap"] + 1;
            //File.WriteAllText(Server.MapPath("Count_Visited.txt"), Application["DaTruyCap"].ToString());
            //Application["PageView"] = (int)Application["PageView"] + 1;
            //Application["Online"] = (int)Application["Online"] + 1;
            Application.UnLock();
        }

        protected void Session_End()
        {
            Application.Lock();
            Application["DangTruyCap"] = (int)Application["DangTruyCap"] - 1;
            Application.UnLock();
        }
    }
}