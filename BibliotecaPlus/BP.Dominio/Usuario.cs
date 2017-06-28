using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using BP.Resources;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
	public class Usuario
	{
		// Usuário
        [Required]
        public virtual string Login { get; set; }
        [Required]
		public virtual string Senha { get; set; }
        public virtual string Roles { get; set; }

        // Pessoa
        [Required]
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual long Cpf { get; set; }
        public virtual DateTime DataNascimento { get; set; }

        // Telefone
        public virtual int Ddd { get; set; }
        public virtual long NumeroTelefone { get; set; }

        // Endereço
        [Required]
        public virtual int Cep { get; set; }
        [Required]
        public virtual string Logradouro { get; set; }
        [Required]
        public virtual string NumeroEndereco { get; set; }
        [Required]
        public virtual string Bairro { get; set; }
        [Required]
        public virtual string Cidade { get; set; }
        [Required]
        public virtual string Estado { get; set; }
	}
}