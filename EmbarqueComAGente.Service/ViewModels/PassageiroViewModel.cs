using QueroBilhete.Infra.Utilities.ExtensionMethods;
using System;
using System.ComponentModel.DataAnnotations;

namespace QueroBilhete.Service.ViewModels
{
    public class PassageiroViewModel
    {
        private string _nome;
        private string _rg;
        private string _cpf;
        private string _telefone;
        private string _celular;
        private string _email;
        private string _cep;
        private string _estado;
        private string _cidade;
        private string _bairro;
        private string _rua;


        public int? Codigo { get; set; }

        [Display(Name = "Nome Completo", Description = "Nome e Sobrenome.")]
        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value.RemoveAcentos(); }
        }

        public string RG
        {
            get { return _rg; }
            set { _rg = value.ApenasNumeros(); }
        }

        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value.ApenasNumeros(); }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value.ApenasNumeros(); }
        }

        public string Celular
        {
            get { return _celular; }
            set { _celular = value.ApenasNumeros(); }
        }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "O E-MAIL do usuário é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Email deve ter no mínimo 5 e no máximo 100 caracteres.")]
        public string Email
        {
            get { return _email; }
            set { _email = value.ToLower(); }
        }

        [StringLength(10, ErrorMessage = "O CEP não pode exceder 10 caracteres ")]
        public string CEP
        {
            get { return _cep; }
            set { _cep = value.ApenasNumeros(); }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value.ApenasNumeros(); }
        }

        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value.RemoveAcentos(); }
        }

        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value.RemoveAcentos(); }
        }

        public string Rua
        {
            get { return _rua; }
            set { _rua = value.RemoveAcentos(); }
        }

        public int? Numero { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
