using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherAssistant.Models
{
    [Table("Material")]
    public class Material
    {
        public Material()
        {
            DataInclusao = DateTime.Now;
        }

        public long Id { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "URL")]
        public string Url { get; set; }
        [Display(Name = "Tipo do material")]
        [EnumDataType(typeof(TipoMaterial))]
        public byte Tipo { get; set; }
        [Display(Name = "Disponibilizar este material?")]
        public bool Visivel { get; set; }

        public DateTime DataInclusao { get; set; }

        public virtual Usuario Professor { get; set; }
        public virtual Usuario Aluno { get; set; }
        public virtual Aula Aula { get; set; }
        public virtual Licao Licao { get; set; }

        // Uma entidade Usuário x Material - Data Liberação, Número Visualizações, Visível, etc...
        public virtual IList<Usuario> Curtidas { get; set; }
    }
}