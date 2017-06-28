using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TeacherAssistant
{
    public enum TipoUsuario : byte
    {
        Aluno = 0,
        Professor = 1
    }

    public enum TipoMaterial : byte
    {
        [Description("Documento")]
        Documento = 0,
        [Description("Referência")]
        Referencia = 1,
        [Description("Vídeo do Youtube")]
        Youtube = 2,
        [Description("Anexo")]
        Anexo = 3
    }

    public enum OrdenacaoMaterial : byte
    {
        Curtidas = 0,
        DataInclusao = 1,
    }
}