using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "EMBARCACAO")]
    public class Embarcacao : Entity
    {
        [Column("ID_EMPRESA")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true, ChaveEstrangeira = "EMPRESA")]
        public int CodigoEmpresa { get; set; }

        [Column("NOME")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Nome { get; set; }

    }
}
