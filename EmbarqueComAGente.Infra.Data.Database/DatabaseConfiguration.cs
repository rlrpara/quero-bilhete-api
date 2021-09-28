using Dapper;
using QueroBilhete.Domain.Entities;
using QueroBilhete.Infra.Data.Context;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Text;
using AutoMapper.Configuration;

namespace QueroBilhete.Infra.Data.Database
{
    public static class DatabaseConfiguration
    {
        #region Métodos Privados
        private static string ObterNomeBanco()
            => Environment.GetEnvironmentVariable("MYSQL_DATABASE");
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
        private static string ObterProcedureDropConstraint(string nomeBanco)
        {
            var sqlComando = new StringBuilder();

            sqlComando.AppendLine($"USE {nomeBanco};");
            sqlComando.AppendLine($"DROP PROCEDURE IF EXISTS PROC_DROP_FOREIGN_KEY;");
            sqlComando.AppendLine($"CREATE PROCEDURE PROC_DROP_FOREIGN_KEY(IN tableName VARCHAR(64), IN constraintName VARCHAR(64))");
            sqlComando.AppendLine($"BEGIN");
            sqlComando.AppendLine($"    IF EXISTS(");
            sqlComando.AppendLine($"        SELECT * FROM information_schema.table_constraints");
            sqlComando.AppendLine($"        WHERE ");
            sqlComando.AppendLine($"            table_schema    = DATABASE()     AND");
            sqlComando.AppendLine($"            table_name      = tableName      AND");
            sqlComando.AppendLine($"            constraint_name = constraintName AND");
            sqlComando.AppendLine($"            constraint_type = 'FOREIGN KEY')");
            sqlComando.AppendLine($"    THEN");
            sqlComando.AppendLine($"        SET @query = CONCAT('ALTER TABLE ', tableName, ' DROP FOREIGN KEY ', constraintName, ';');");
            sqlComando.AppendLine($"        PREPARE stmt FROM @query; ");
            sqlComando.AppendLine($"        EXECUTE stmt; ");
            sqlComando.AppendLine($"        DEALLOCATE PREPARE stmt; ");
            sqlComando.AppendLine($"    END IF; ");
            sqlComando.AppendLine($"END");

            return sqlComando.ToString();
        }
        private static string ObterSqlCriarBanco(string nomeBanco)
        {
            var sqlComando = new StringBuilder();

            sqlComando.AppendLine($"CREATE DATABASE {nomeBanco}");
            sqlComando.AppendLine($"CHARACTER SET utf8");
            sqlComando.AppendLine($"COLLATE utf8_general_ci;");

            return sqlComando.ToString();
        }
        private static bool ExisteBanco(MySqlConnection conexao, string nomeBanco)
            => conexao.Query<string>($"SHOW DATABASES LIKE '{nomeBanco}';").ToList().Count > 0;
        private static void Criar(MySqlConnection conexao, string sqlCondicao)
                    => conexao.Execute(sqlCondicao);
        #endregion

        #region Métodos Públicos
        public static void GerenciarBanco()
        {
            try
            {
                var nomeBanco = ObterNomeBanco();
                using var conexao = new MySqlConnection(ObterStringConexao());

                //Criar banco
                if (!ExisteBanco(conexao, nomeBanco))
                    Criar(conexao, ObterSqlCriarBanco(nomeBanco));

                //Criar tabelas
                Criar(conexao,ObterProcedureDropConstraint(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<Empresa>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<Embarcacao>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<Usuario>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<Trajeto>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<Viagem>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<TipoPassagem>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<Passageiro>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<Passagem>(nomeBanco));
                Criar(conexao,GeradorDapper.CriarTabela<VersaoBanco>(nomeBanco));

                //Criar procedures
                Criar(conexao, GeradorDapper.GerarProcedureAddIfColumnNotExists(nomeBanco));

                //executar scripts da versao
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
