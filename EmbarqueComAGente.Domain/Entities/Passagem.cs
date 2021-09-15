using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "PASSAGEM")]
    public class Passagem : Entity
    {
        [Column("ID_TIPO_PASSAGEM")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true, ChaveEstrangeira = "TIPO_PASSAGEM")]
        public int CodigoTipoPassagem { get; set; }

        [Column("ID_VIAGEM")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true, ChaveEstrangeira = "VIAGEM")]
        public int CodigoViagem { get; set; }

        [Column("ID_PASSAGEIRO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true, ChaveEstrangeira = "PASSAGEIRO")]
        public int CodigoPassageiro { get; set; }

        [Column("DATA_AGENDAMENTO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime? DataAgendamento { get; set; }

        [Column("DATA_REMARCACAO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime? DataRemarcacao { get; set; }

        [Column("DATA_EMBARQUE")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime? DataEmbarque { get; set; }

        [Column("STATUS")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public int StatusViagem { get; set; }
    }
}
