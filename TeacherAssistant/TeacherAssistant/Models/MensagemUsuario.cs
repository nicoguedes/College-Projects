using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Models
{
    public class MensagemUsuario
    {
        public long Id { get; set; }
        [Display(Name = "Assunto")]
        public string Assunto { get; set; }
        [Display(Name = "Mensagem")]
        public string Texto { get; set; }
        public bool Lida { get; set; }

        public DateTime DataEnvio { get; set; }
        public virtual Usuario Remetente { get; set; }
        public virtual Usuario Destinatario { get; set; }
    }
}