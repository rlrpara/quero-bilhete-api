using System;

namespace QueroBilhete.Service.ViewModels
{
    public class ViagemViewModel
    {
        public int? Codigo { get; set; }
        public int CodigoTrajeto { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
