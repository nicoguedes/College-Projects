using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Models
{
    [Table("Mensagem")]
    public class Mensagem
    {
        public long Id { get; set; }

        [Display(Name = "Assunto")]
        public string Assunto { get; set; }
        [Required]
        [Display(Name = "Mensagem")]
        public string Texto { get; set; }

        public DateTime DataEnvio { get; set; }

        public virtual Forum Forum { get; set; }
        public virtual Mensagem Pai { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual IList<Mensagem> Filhos { get; set; }

        public Mensagem()
        {
            this.Filhos = new List<Mensagem>();
        }
    }
}
