using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BP.Web.Models
{
    public class BPAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!UsuarioLogado.IsUsuarioLogado)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "Index", controller = "Login", area = "" }));
                return;
            }

            if (!string.IsNullOrEmpty(this.Roles))
                if (!(UsuarioLogado.Usuario.Roles ?? "").Contains(this.Roles))
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "AcessoNaoAutorizado", controller = "Login", area = "" }));
        }
    }
}