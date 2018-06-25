using System.Web;
using System.Web.Mvc;

namespace DSV.Webservice.Central.DMSRetrieval
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}