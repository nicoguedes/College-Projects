using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;

namespace TeacherAssistant.Controllers
{
    public class ResponderQuestionarioController : BaseController
    {
        DbEntidades db = new DbEntidades();

        //
        // GET: /ResponderQuestionario/

        public ActionResult Index(long? idLicao, long? idAula)
        {
            if (idLicao.HasValue)
            {
                Licao licao = db.Licoes.Find(idLicao.Value);
                return View(licao.Questionario);
            }
            else if (idAula.HasValue)
            {
                Aula aula = db.Aulas.Find(idAula.Value);
                return View(aula.Questionario);
            }
            else
                throw new ApplicationException("Deve ser informada uma aula ou lição.");
        }

    }
}
