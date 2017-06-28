using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public class RespostaQuestaoAluno
    {
        public string RespostaMarcada { get; set; }
        public bool IsRespostaCorreta { get; set; }
        
        public virtual QuestionarioAluno QuestionarioRespondido { get; set; }
        public virtual Questao Questao { get; set; }
    }
}