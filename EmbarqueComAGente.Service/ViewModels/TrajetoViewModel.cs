using QueroBilhete.Infra.Utilities.ExtensionMethods;
using System;

namespace QueroBilhete.Service.ViewModels
{
    public class TrajetoViewModel
    {
        private string _origem;
        private string _destino;



        public int? Codigo { get; set; }
        public int CodigoEmbarcacao { get; set; }
        public string Origem
        {
            get { return _origem; }
            set { _origem = value.RemoveAcentos(); }
        }
        public string Destino
        {
            get { return _destino; }
            set { _destino = value.RemoveAcentos(); }
        }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

    }
}
