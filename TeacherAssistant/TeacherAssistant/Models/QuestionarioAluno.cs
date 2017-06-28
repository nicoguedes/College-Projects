using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public class QuestionarioAluno
    {
        public long Id { get; set; }
        public IList<RespostaQuestaoAluno> Respostas { get; set; }
        public bool Aprovado { get; set; }

        public DateTime DataEnvio { get; set; }
        
        public virtual Aula Aula { get; set; }
        public virtual Licao Licao { get; set; }
        public virtual Usuario Aluno { get; set; }
    }
}