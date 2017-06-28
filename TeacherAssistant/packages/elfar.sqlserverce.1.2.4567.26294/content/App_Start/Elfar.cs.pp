using System.Web.Mvc;
using System.Web.Routing;
using Elfar;
using Elfar.SqlServerCe;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.Elfar), "Init")]
namespace $rootnamespace$.App_Start
{
    public static class Elfar
    {
        public static void Init()
        {
            var provider = new SqlCeErrorLogProvider();
            GlobalFilters.Filters.Add(new ErrorLogFilter(provider));
            RouteTable.Routes.Insert(0, new ErrorLogRoute(provider));
        }
    }
}