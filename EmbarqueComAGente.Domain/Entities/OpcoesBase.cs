using System;

namespace QueroBilhete.Domain.Entities
{
    public class OpcoesBase : Attribute
    {
        public bool ChavePrimaria { get; set; }
        public bool UsarNoBancoDeDados { get; set; }
        public bool UsarParaBuscar { get; set; }
        public string ChaveEstrangeira { get; set; }
    }
}
