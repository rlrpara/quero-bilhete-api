using QueroBilhete.Infra.Utilities.ExtensionMethods;
using System;
using System.ComponentModel.DataAnnotations;

namespace QueroBilhete.Service.ViewModels
{
    public class UsuarioViewModel
    {
        private string _nome;
        private string _email;
        private string _senha;
        private string _cep;
        private string _estado;
        private string _cidade;
        private string _bairro;
        private string _rua;



        public int? Codigo { get; set; }
        public string Uid { get; set; }

        [Display(Name = "Nome Completo", Description = "Nome e Sobrenome.")]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value.RemoveAcentos(); }
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
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        public int CodigoNivelAcesso { get; set; }

        [StringLength(10, ErrorMessage = "O CEP não pode exceder 10 caracteres ")]
        public string Cep
        {
            get { return _cep; }
            set { _cep = value.ApenasNumeros(); }
        }
        [StringLength(4, ErrorMessage = "O Estado não pode exceder 2 caracteres. ")]
        public string Estado
        {
            get { return _estado; }
            set { _estado = value.RemoveAcentos(); }
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
        public bool NovoUsuario { get; set; } = false;
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
