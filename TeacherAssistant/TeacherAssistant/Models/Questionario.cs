using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherAssistant.Models
{
    [Table("Questionario")]
    public class Questionario
    {
        public long Id { get; set; }
        
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Display(Name = "Erros Permitidos")]
        public int ErrosPermitidos { get; set; }
        [Display(Name = "Quantidade de Questões")]
        public int QuantidadeQuestoes { get; set; }

        public virtual Usuario Professor { get; set; }

        public virtual IList<Questao> Questoes { get; set; }
        public virtual IList<Licao> Licoes { get; set; }
        public virtual IList<Licao> Aulas { get; set; }

    }
}