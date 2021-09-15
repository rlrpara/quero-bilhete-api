using QueroBilhete.Infra.Utilities.ExtensionMethods;
using System;
using System.ComponentModel.DataAnnotations;

namespace QueroBilhete.Service.ViewModels
{
    public class EmpresaViewModel
    {
        private string _cnpj;
        private string _razaoSocial;
        private string _telefone;
        private string _celular;
        private string _email;
        private string _inscricaoEstadual;
        private string _inscricaoMunicipal;
        private DateTime _dataCadastro = DateTime.Now;
        private DateTime _dataAtualizacao = DateTime.Now;
        private string _site;
        private string _cep;
        private string _estado;
        private string _cidade;
        private string _bairro;
        private string _rua;
        private int? _numero;


        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value.RemoveAcentos(); }
        }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value.ApenasNumeros(); }
        }

        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set { _inscricaoEstadual = value.ApenasNumeros(); }
        }

        public string InscricaoMunicipal
        {
            get { return _inscricaoMunicipal; }
            set { _inscricaoMunicipal = value.ApenasNumeros(); }
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

        public string Email
        {
            get { return _email; }
            set { _email = value.ToLower() ; }
        }

        public string Site
        {
            get { return _site; }
            set { _site = value.ToLower(); }
        }

        public string Logo { get; set; }

        [StringLength(10, ErrorMessage = "O {0} não pode exceder {1} characteres. ")]
        public string CEP
        {
            get { return _cep; }
            set { _cep = value.ApenasNumeros(); }
        }

        [StringLength(2, ErrorMessage = "O {0} não pode exceder {1} characteres. ")]
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

        public int? Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro
        {
            get { return _dataCadastro; }
            set { _dataCadastro = value.AjustaData(); }
        }
        public DateTime DataAtualizacao
        {
            get { return _dataAtualizacao; }
            set { _dataAtualizacao = value.AjustaData(); }
        }
    }
}
