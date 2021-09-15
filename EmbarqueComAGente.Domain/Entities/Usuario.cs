using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    [Table(name: "USUARIO")]
    public class Usuario : Entity
    {
        [Column("UID")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Uid { get; set; }

        [Column("NOME")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Nome { get; set; }

        [Column("EMAIL")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Email { get; set; }

        [Column("SENHA")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public string Senha { get; set; }

        [Column("ID_NIVEL_ACESSO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public int CodigoNivelAcesso { get; set; }

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
