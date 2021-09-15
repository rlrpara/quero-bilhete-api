using System;

namespace QueroBilhete.Service.ViewModels
{
    public class PassagemViewModel
    {
        public int? Codigo { get; set; }
        public int CodigoTipoPassagem { get; set; }
        public int CodigoViagem { get; set; }
        public int CodigoPassageiro { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public DateTime? DataRemarcacao { get; set; }
        public DateTime? DataEmbarque { get; set; }
        public int StatusViagem { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
