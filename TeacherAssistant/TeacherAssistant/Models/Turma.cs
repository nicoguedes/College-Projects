using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherAssistant.Models
{
    [Table("Turma")]
    public class Turma
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public virtual Usuario Professor { get; set; }

        public virtual IList<Usuario> Alunos { get; set; }
    }
}