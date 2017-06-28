using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeacherAssistant.Models;

namespace TeacherAssistant
{
    public class UsuarioLogado
    {
        public static Usuario User
        {
            get { return HttpContext.Current.Session["usuario_logado"] as Usuario; }
            set { HttpContext.Current.Session["usuario_logado"] = value; }
        }

        public static Turma Turma
        {
            get { return HttpContext.Current.Session["turma_selecionada"] as Turma; }
            set { HttpContext.Current.Session["turma_selecionada"] = value; }
        }
    }
}