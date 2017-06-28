using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TeacherAssistant.Util;
using System.Data.Entity;

namespace TeacherAssistant
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

            BundleTable.EnableOptimizations = false;
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer<DbEntidades>(new SchoolInitializer());


            DbEntidades db = new DbEntidades();

            // Se não existir nenhum professor cadastrado, força o cadastro de um
            if (!db.Usuarios.Where(m => m.Tipo == (byte)TipoUsuario.Professor).Any())
            {
                var administrador = new Models.Usuario()
                {
                    Login = "admin",
                    Senha = Criptografia.CriptografarMd5("123"),
                    Nome = "Administrador do Sistema",
                    Tipo = (byte)TipoUsuario.Professor
                };

                db.Usuarios.Add(administrador);
                db.SaveChanges();
            }
        }
    }
}