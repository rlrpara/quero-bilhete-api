using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "TRAJETO")]
    public class Trajeto : Entity
    {
        [Column("ID_EMBARCACAO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true, ChaveEstrangeira = "EMBARCACAO")]
        public int CodigoEmbarcacao { get; set; }

        [Column("ORIGEM")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Origem { get; set; }

        [Column("DESTINO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Destino { get; set; }

    }
}
