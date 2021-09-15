using QueroBilhete.Domain.Entities;
using QueroBilhete.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueroBilhete.Infra.Data.Repositories
{
    public class ViagemRepository : BaseRepository, IViagemRepository
    {
        private readonly IBaseRepository _repositorio;

        public ViagemRepository(IBaseRepository repository)
        {
            _repositorio = repository;
        }

        public List<DadosConsultaViagem> PostConsultaViagem(DateTime? DataIda, DateTime? DataVolta, string Origem, string Destino, int? PaginaAtual, int? QuantidadePagina)
        {
            var posicao = 0;

            if(PaginaAtual > 0) posicao = (int)(((QuantidadePagina ?? 0) * ((PaginaAtual ?? 0) - 1)));

            var sqlPesquisa = new StringBuilder();

            sqlPesquisa.AppendLine($"SELECT CodigoViagem,");
            sqlPesquisa.AppendLine($"       DataViagem,");
            sqlPesquisa.AppendLine($"       HoraViagem,");
            sqlPesquisa.AppendLine($"       Origem,");
            sqlPesquisa.AppendLine($"       Destino,");
            sqlPesquisa.AppendLine($"       NomeEmbarcacao,");
            sqlPesquisa.AppendLine($"       NomeEmpresa,");
            sqlPesquisa.AppendLine($"       Logomarca,");
            sqlPesquisa.AppendLine($"       Duracao,");
            sqlPesquisa.AppendLine($"       TipoClasse,");
            sqlPesquisa.AppendLine($"       TipoViagem");
            sqlPesquisa.AppendLine($"FROM");
            sqlPesquisa.AppendLine($"  (SELECT DISTINCT viagem.ID AS CodigoViagem,");
            sqlPesquisa.AppendLine($"                   viagem.DATA AS DataViagem,");
            sqlPesquisa.AppendLine($"                   viagem.HORA AS HoraViagem,");
            sqlPesquisa.AppendLine($"                   trajeto.ORIGEM AS Origem,");
            sqlPesquisa.AppendLine($"                   trajeto.DESTINO AS Destino,");
            sqlPesquisa.AppendLine($"                   embarcacao.NOME AS NomeEmbarcacao,");
            sqlPesquisa.AppendLine($"                   empresa.RAZAO_SOCIAL AS NomeEmpresa,");
            sqlPesquisa.AppendLine($"                   empresa.LOGO AS Logomarca,");
            sqlPesquisa.AppendLine($"                   '12h30min' AS Duracao,");
            sqlPesquisa.AppendLine($"                   'Normal' AS TipoClasse,");
            sqlPesquisa.AppendLine($"                   1 AS TipoViagem");
            sqlPesquisa.AppendLine($"              FROM viagem");
            sqlPesquisa.AppendLine($"              JOIN trajeto ON trajeto.ID = viagem.ID_TRAJETO");
            sqlPesquisa.AppendLine($"              JOIN embarcacao ON embarcacao.ID = trajeto.ID_EMBARCACAO");
            sqlPesquisa.AppendLine($"              JOIN empresa ON empresa.ID = embarcacao.ID_EMPRESA");
            sqlPesquisa.AppendLine($"             WHERE 0 = 0 ");
            if (DataIda >= DateTime.Now.AddDays(1) && DataVolta >= DateTime.Now.AddDays(1))
                sqlPesquisa.AppendLine($" AND CAST(DATA AS date) BETWEEN '{DataIda?.ToString("yyyy-MM-dd")}' AND '{DataVolta?.ToString("yyyy-MM-dd")}'");
            else if (DataIda >= DateTime.Now.AddDays(1))
                sqlPesquisa.AppendLine($" AND CAST(DATA AS date) >= '{DataIda?.ToString("yyyy-MM-dd")}'");
            else if (DataVolta >= DateTime.Now.AddDays(1))
                sqlPesquisa.AppendLine($" AND CAST(DATA AS date) >= '{DataVolta?.ToString("yyyy-MM-dd")}'");
            if (!string.IsNullOrWhiteSpace(Origem))
                sqlPesquisa.AppendLine($"   AND trajeto.ORIGEM LIKE '{Origem}'");
            if (!string.IsNullOrWhiteSpace(Destino))
                sqlPesquisa.AppendLine($"          AND trajeto.DESTINO LIKE '{Destino}'");
            sqlPesquisa.AppendLine($"              AND viagem.ATIVO = 1");
            if (DataVolta >= DateTime.Now.AddDays(1))
            {
                sqlPesquisa.AppendLine($"   UNION SELECT viagem.ID AS CodigoViagem,");
                sqlPesquisa.AppendLine($"                viagem.DATA AS DataViagem,");
                sqlPesquisa.AppendLine($"                viagem.HORA AS HoraViagem,");
                sqlPesquisa.AppendLine($"                trajeto.ORIGEM AS Origem,");
                sqlPesquisa.AppendLine($"                trajeto.DESTINO AS Destino,");
                sqlPesquisa.AppendLine($"                embarcacao.NOME AS NomeEmbarcacao,");
                sqlPesquisa.AppendLine($"                empresa.RAZAO_SOCIAL AS NomeEmpresa,");
                sqlPesquisa.AppendLine($"                empresa.LOGO AS Logomarca,");
                sqlPesquisa.AppendLine($"                '12h30min' AS Duracao,");
                sqlPesquisa.AppendLine($"                'Normal' AS TipoClasse,");
                sqlPesquisa.AppendLine($"                2 AS TipoViagem");
                sqlPesquisa.AppendLine($"           FROM viagem");
                sqlPesquisa.AppendLine($"           JOIN trajeto ON trajeto.ID = viagem.ID_TRAJETO");
                sqlPesquisa.AppendLine($"           JOIN embarcacao ON embarcacao.ID = trajeto.ID_EMBARCACAO");
                sqlPesquisa.AppendLine($"           JOIN empresa ON empresa.ID = embarcacao.ID_EMPRESA");
                sqlPesquisa.AppendLine($"          WHERE 0=0");
                if (DataIda >= DateTime.Now.AddDays(1))
                    sqlPesquisa.AppendLine($"            AND CAST(DATA AS date) >= '{DataVolta?.ToString("yyyy-MM-dd")}'");
                if (!string.IsNullOrWhiteSpace(Destino))
                    sqlPesquisa.AppendLine($"            AND trajeto.ORIGEM LIKE '{Destino}'");
                if (!string.IsNullOrWhiteSpace(Origem))
                    sqlPesquisa.AppendLine($"            AND trajeto.DESTINO LIKE '{Origem}'");
                sqlPesquisa.AppendLine($"            AND viagem.ATIVO = 1");
            }
            sqlPesquisa.AppendLine($"            ) AS PESQUISA");
            sqlPesquisa.AppendLine($" LIMIT {posicao}, {QuantidadePagina}");

            return _repositorio.BuscarTodosPorQuery<DadosConsultaViagem>(sqlPesquisa.ToString()).ToList();
        }
    }
}
