using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeacherAssistant.Models;

namespace TeacherAssistant.ViewModels
{
    public class DadosHomePage
    {
        public IList<Licao> Licoes { get; set; }
        public IList<MensagemUsuario> MensagensNaoLidas { get; set; }


        public DadosHomePage() {
            Licoes = new List<Licao>();
            MensagensNaoLidas = new List<MensagemUsuario>();
        }
    }
}