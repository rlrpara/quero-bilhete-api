using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "VIAGEM")]
    public class Viagem : Entity
    {
        [Column("ID_TRAJETO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true, ChaveEstrangeira = "TRAJETO")]
        public int CodigoTrajeto { get; set; }

        [Column("DATA")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime Data { get; set; }

        [Column("HORA")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime Hora { get; set; }
    }
}
