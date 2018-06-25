using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using DSV.Webservice.Central.DMSRetrieval.log4Net;

namespace DSV.Webservice.Central.DMSRetrieval
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801



    /// <summary>
    /// WebApiApplication
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {

        /// <summary>
        /// Name used to identify this application in e.g. the log file.
        /// </summary>
        public const string APPLICATION_NAME = "DSV.Webservice.Central.DMSRetrieval";


        //----------------------------------------------
        //METHOD : Application_Start
        //----------------------------------------------
        /// <summary>
        /// Application_Start
        /// </summary>
        protected void Application_Start()
        {
            LogApplicationNameAndVersionNumber();

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }




        //-------------------------------------------------------------
        //METHOD : LogApplicationNameAndVersionNumber
        //-------------------------------------------------------------
        /// <summary>
        /// LogApplicationNameAndVersionNumber
        /// </summary>
        private static void LogApplicationNameAndVersionNumber()
        {
            try
            {
                Assembly asm;
                string ver = "?.?.?.?";

                // first thing logged is the application name/version...
                asm = Assembly.GetAssembly(typeof(WebApiApplication));
                if (asm != null)
                {
                    ver = asm.GetName().Version.ToString();
                }
                Logger.Info("========================================================================");
                Logger.Info("Welcome to " + APPLICATION_NAME + ", version " + ver);
                Logger.Info("========================================================================");
            }
            catch
            {

            }
        }




    }
}