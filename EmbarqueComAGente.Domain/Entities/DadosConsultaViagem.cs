using System;

namespace QueroBilhete.Domain.Entities
{
    public class DadosConsultaViagem
    {
        public int CodigoViagem { get; set; }
        public DateTime? DataViagem { get; set; }
        public DateTime? HoraViagem { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string NomeEmbarcacao { get; set; }
        public string NomeEmpresa { get; set; }
        public string Logomarca { get; set; }
        public string Duracao { get; set; }
        public string TipoClasse { get; set; }
        public int TipoViagem { get; set; }
    }
}
