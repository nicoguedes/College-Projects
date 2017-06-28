using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherAssistant.Models
{
    [Table("Questao")]
    public class Questao
    {
        public long Id { get; set; }

        [Display(Name = "Enunciado")]
        public string Enunciado { get; set; }
        [Display(Name = "Resposta Correta")]
        [Required]
        public string RespostaCorreta { get; set; }
        [Display(Name = "Alternativa A)")]
        public string AlternativaA { get; set; }
        [Display(Name = "Alternativa B)")]
        public string AlternativaB { get; set; }
        [Display(Name = "Alternativa C)")]
        public string AlternativaC { get; set; }
        [Display(Name = "Alternativa D)")]
        public string AlternativaD { get; set; }
        [Display(Name = "Alternativa E)")]
        public string AlternativaE { get; set; }

        [Display(Name = "#Tag (Categoria)")]
        [Required]
        public string Tag { get; set; }

        
        // Campo de tela para ver se a questão está selecionada no questionário:
        [NotMapped]
        public bool Selected { get; set; }


        public virtual Usuario Professor { get; set; }

        public virtual IList<Questionario> Questionarios { get; set; }
    }
}
