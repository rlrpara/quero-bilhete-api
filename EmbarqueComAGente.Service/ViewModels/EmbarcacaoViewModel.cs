using QueroBilhete.Infra.Utilities.ExtensionMethods;
using System;

namespace QueroBilhete.Service.ViewModels
{
    public class EmbarcacaoViewModel
    {
        private string _nome;


        public int? Codigo { get; set; }
        public int CodigoEmpresa { get; set; }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value.RemoveAcentos(); }
        }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

    }
}
