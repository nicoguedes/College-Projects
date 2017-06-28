using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio;

namespace BP.Web.Models
{
	public sealed class UsuarioLogado
	{
		public static bool IsUsuarioLogado
		{
			get
			{
				return HttpContext.Current.Session["UsuarioLogado"] != null;
			}
		}

		public static Usuario Usuario
		{
			get
			{
				return IsUsuarioLogado ? (Usuario)HttpContext.Current.Session["UsuarioLogado"] : null;
			}
			set 
			{
				HttpContext.Current.Session["UsuarioLogado"] = value;
			}
		}
	}
}