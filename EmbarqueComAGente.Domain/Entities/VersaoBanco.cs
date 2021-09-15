using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "VERSAO_BANCO")]
    public class VersaoBanco : Entity
    {
        [Column("VERSAO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Versao { get; set; }

        [Column("DATA_GERADA")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime? DataGerada { get; set; } = DateTime.Now;
    }
}
