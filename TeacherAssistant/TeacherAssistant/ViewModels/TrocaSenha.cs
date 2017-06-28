using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.ViewModels
{
    public class TrocaSenha
    {
        [Display(Name = "Senha Atual")]
        [Required]
        public string SenhaAtual { get; set; }
        [Required]
        [Display(Name = "Nova Senha")]
        public string NovaSenha { get; set; }
        [Required]
        [Display(Name = "Confirme a Nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não correspondem")]
        public string ConfirmacaoNovaSenha { get; set; }
    }
}