using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace QueroBilhete.Infra.Data.Context
{
    public static class ConnectionConfiguration
    {
        #region Métodos Privados
        private static ParametrosConexao ObterParametrosConexao() => new ParametrosConexao()
        {
            ServidorOnline = Environment.GetEnvironmentVariable("MYSQL_SERVER_ONLINE"),
            ServidorLocal = Environment.GetEnvironmentVariable("MYSQL_SERVER_LOCAL"),
            TipoAcesso = Convert.ToInt32(Environment.GetEnvironmentVariable("MYSQL_TIPO")),
            Usuario = Environment.GetEnvironmentVariable("MYSQL_USER"),
            Senha = Environment.GetEnvironmentVariable("MYSQL_PASSWORD"),
            NomeBanco = Environment.GetEnvironmentVariable("MYSQL_DATABASE"),
            Porta = Convert.ToInt32(Environment.GetEnvironmentVariable("MYSQL_PORT")),
        };
        private static string ObterStringConexao()
        {
            var conexao = ObterParametrosConexao();

            return $"Server={(conexao.TipoAcesso == 1 ? conexao.ServidorOnline : conexao.ServidorLocal)}; User Id={conexao.Usuario}; Password={conexao.Senha}; Allow User Variables=True";
        }
        private static void Termina(IDbConnection conexao)
        {
            if (conexao != null && conexao.State == ConnectionState.Open)
                conexao.Close();
        }
        #endregion

        #region Métodos Públicos
        public static IDbConnection ObterConexao()
        {
            IDbConnection conexao;

            conexao = new MySqlConnection(ObterStringConexao());

            Inicia(conexao);

            return conexao;
        }
        public static void Inicia(IDbConnection connection)
        {
            Termina(connection);

            if (connection != null && connection.State == ConnectionState.Closed)
                connection.Open();
        }
        #endregion
    }
}
