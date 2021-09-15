using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueroBilhete.Domain.Entities
{
    public class Entity : OpcoesBase
    {
        [Key]
        [Column("ID")]
        [OpcoesBase(UsarNoBancoDeDados = true, ChavePrimaria = true, UsarParaBuscar = true)]
        public int? Codigo { get; set; }

        [Column("ATIVO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public bool Ativo { get; set; }

        [Column("DATA_CADASTRO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Column("DATA_ATUALIZACAO")]
        [OpcoesBase(UsarNoBancoDeDados = true, UsarParaBuscar = true)]
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
