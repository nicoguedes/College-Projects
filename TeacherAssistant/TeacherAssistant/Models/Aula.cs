using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherAssistant.Models
{
    [Table("Aula")]
    public class Aula
    {
        public long Id { get; set; }
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public virtual Forum Forum { get; set; }
        public virtual Licao Licao { get; set; }

        [Display(Name = "Início do Questionário")]
        public DateTime? DataInicioQuestionario { get; set; }
        [Display(Name = "Limite do Questionário")]
        public DateTime? DataLimiteQuestionario { get; set; }

        [Display(Name = "Questionário")]
        public virtual Questionario Questionario { get; set; }

        public virtual IList<Material> Materiais { get; set; }

        public IList<Material> GetMateriaisVisiveis()
        {
            return this.Materiais.Where(m => m.Visivel).ToList();
        }
    }
}