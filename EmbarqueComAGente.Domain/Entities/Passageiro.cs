using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "PASSAGEIRO")]
    public class Passageiro : Entity
    {
        [Column("NOME")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Nome { get; set; }

        [Column("RG")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string RG { get; set; }

        [Column("CPF")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string CPF { get; set; }

        [Column("TELEFONE")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Telefone { get; set; }

        [Column("CELULAR")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Celular { get; set; }

        [Column("EMAIL")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Email { get; set; }

        [Column("CEP")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Cep { get; set; }

        [Column("ESTADO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Estado { get; set; }

        [Column("CIDADE")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Cidade { get; set; }

        [Column("BAIRRO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Bairro { get; set; }

        [Column("RUA")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Rua { get; set; }

        [Column("NUMERO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public int? Numero { get; set; }
    }
}
