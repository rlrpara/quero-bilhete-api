using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "TIPO_PASSAGEM")]
    public class TipoPassagem : Entity
    {
        [Column("DESCRICAO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Descricao { get; set; }
    }
}
