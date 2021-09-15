using System;

namespace QueroBilhete.Service.ViewModels.Consulta
{
    public class ConsultaViagemOrigemDestino
    {
        public DateTime? DataIda { get; set; }
        public DateTime? DataVolta { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int PaginaAtual { get; set; } = 1;
        public int QuantidadePorPagina { get; set; } = 10;
    }
}
