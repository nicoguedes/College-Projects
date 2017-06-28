using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherAssistant.Models
{
    [Table("Forum")]
    public class Forum
    {
        public long Id { get; set; }
        public DateTime DataCriacao { get; set; }

        // TODO: verificar mais campos que poderão ser incluídos

        public virtual IList<Mensagem> Mensagens { get; set; }

        public Forum()
        {
            this.Mensagens = new List<Mensagem>();
        }

        public static Forum Create()
        {
            Forum forum = new Forum()
            {
                DataCriacao = DateTime.Now,
                Mensagens = new List<Mensagem>()
            };

            return forum;
        }
    }
}