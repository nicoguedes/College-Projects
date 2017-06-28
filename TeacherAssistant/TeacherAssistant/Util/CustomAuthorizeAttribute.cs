using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;

namespace TeacherAssistant.Util
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private TipoUsuario? tipoUsuarioAutorizado;

        public CustomAuthorizeAttribute() { }

        public CustomAuthorizeAttribute(TipoUsuario tipoUsuarioAutorizado)
        {
            this.tipoUsuarioAutorizado = tipoUsuarioAutorizado;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string activeController = Convert.ToString(filterContext.RouteData.Values["controller"]);
            bool ignorarRedirecionamentoLogin = activeController == "Login";
            bool ignorarRedirecionamentoTurma = activeController == "SelecionarTurma" || activeController == "Turma";

            if (UsuarioLogado.User == null && !ignorarRedirecionamentoLogin)
            {
                Redirecionar(filterContext, "Login", "Index");
                return;
            }
            else if (UsuarioLogado.Turma == null && !ignorarRedirecionamentoTurma)
            {
                Redirecionar(filterContext, "SelecionarTurma", "Index");
                return;
            }
            else if (this.tipoUsuarioAutorizado.HasValue && UsuarioLogado.User.Tipo != (byte)this.tipoUsuarioAutorizado)
            {
                Redirecionar(filterContext, "Home", "Index");
                return;
            }
        }

        private void Redirecionar(AuthorizationContext filterContext, string controller, string action, HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
        {
            filterContext.HttpContext.Response.StatusCode = (int)statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            UrlHelper url = new UrlHelper(filterContext.RequestContext);

            filterContext.Result = new RedirectToRouteResult(
                                          new RouteValueDictionary {
                                            { "Controller", controller },
                                            { "Action", action }
                                    });
        }
    }
}