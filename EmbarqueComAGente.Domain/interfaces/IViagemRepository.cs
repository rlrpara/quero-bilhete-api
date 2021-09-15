using QueroBilhete.Domain.Entities;
using System;
using System.Collections.Generic;

namespace QueroBilhete.Domain.interfaces
{
    public interface IViagemRepository : IBaseRepository
    {
        List<DadosConsultaViagem> PostConsultaViagem(DateTime? DataIda, DateTime? DataVolta, string Origem, string Destino, int? NumeroPaginaAtual, int? QuantidadePagina);
    }
}
