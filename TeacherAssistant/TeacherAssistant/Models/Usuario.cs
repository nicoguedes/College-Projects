using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherAssistant.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        private string nome;
        private string caminhoFoto;

        public long Id { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get { return string.IsNullOrEmpty(nome) ? Login : nome; } set { nome = value; } }

        [Display(Name = "Matricula")]
        public string Matricula { get; set; }

        [Display(Name = "Aluno ou professor?")]
        [EnumDataType(typeof(TipoUsuario))]
        public byte Tipo { get; set; }

        [Display(Name = "Foto")]
        public string CaminhoFoto { get { return caminhoFoto; } set { caminhoFoto = string.IsNullOrEmpty(value) ? "~/Content/images/SemFoto.jpg" : value; } }

        public virtual IList<MensagemUsuario> Mensagens { get; set; }
        public virtual IList<Turma> Turmas { get; set; }
        public virtual IList<Material> MateriaisCurtidos { get; set; }
    }
}