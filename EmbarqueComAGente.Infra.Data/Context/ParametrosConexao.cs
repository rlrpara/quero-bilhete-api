namespace QueroBilhete.Infra.Data.Context
{
    internal class ParametrosConexao
    {
        public ParametrosConexao()
        {
        }

        public string ServidorOnline { get; set; }
        public string ServidorLocal { get; set; }
        public int TipoAcesso { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string NomeBanco { get; set; }
        public int Porta { get; set; }
        public int Ambiente { get; set; }
    }
}