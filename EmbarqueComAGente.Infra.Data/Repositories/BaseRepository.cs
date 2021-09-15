using Dapper;
using Dapper.Contrib.Extensions;
using QueroBilhete.Domain.interfaces;
using QueroBilhete.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QueroBilhete.Infra.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        #region Propriedades Privadas
        private readonly IDbConnection _conexao;
        private readonly static string _nomeBanco = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
        #endregion

        #region Construtores
        public BaseRepository()
        {
            _conexao = ConnectionConfiguration.ObterConexao();
        }
        #endregion

        #region Métodos Privados
        private static string ObterNomeTabela<T>()
        {
            SqlMapperExtensions.TableNameMapper = TableNameMapper;
            return TableNameMapper(typeof(T));
        }
        private static string TableNameMapper(Type type)
        {
            dynamic tableattr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute");
            return (tableattr != null ? tableattr.Name : string.Empty);
        }
        private void AbrirConexao()
        {
            FecharConexao();

            if (_conexao.State == ConnectionState.Closed && _conexao.State != ConnectionState.Open)
                _conexao.Open();
        }
        private void FecharConexao()
        {
            if (_conexao.State == ConnectionState.Open && _conexao.State != ConnectionState.Closed)
                _conexao.Close();
        }
        #endregion

        #region Métodos Públicos
        public int Adicionar<T>(T entidade) where T : class
        {
            AbrirConexao();
            return _conexao.Execute(GeradorDapper.RetornaInsert<T>(entidade));
        }

        public int Atualizar<T>(int id, T entidade) where T : class
        {
            AbrirConexao();
            return _conexao.Execute(GeradorDapper.RetornaUpdate(id, entidade));
        }

        public T BuscarPorId<T>(int id) where T : class
        {
            AbrirConexao();
            return _conexao.QueryFirstOrDefault<T>($"{GeradorDapper.RetornaSelect<T>(id)}", commandTimeout: 80000000, commandType: CommandType.Text);
        }

        public T BuscarPorQuery<T>(string query)
        {
            AbrirConexao();
            return _conexao.QueryFirstOrDefault<T>(query, commandTimeout: 80000000, commandType: CommandType.Text);
        }

        public T BuscarPorQueryGerador<T>(string sqlWhere = null) where T : class
        {
            AbrirConexao();
            var sqlPesquisa = new StringBuilder();

            sqlPesquisa.AppendLine($"{GeradorDapper.RetornaSelect<T>()}");

            if (!string.IsNullOrEmpty(sqlWhere)) sqlPesquisa.Append($"AND {sqlWhere}");

            return _conexao.Query<T>(sqlPesquisa.ToString(), commandTimeout: 80000000, commandType: CommandType.Text).FirstOrDefault();
        }

        public IEnumerable<T> BuscarTodosPorQuery<T>(string query = null) where T : class
        {
            AbrirConexao();
            var sqlPesquisa = new StringBuilder();

            if (string.IsNullOrEmpty(query))
                sqlPesquisa.AppendLine($"{GeradorDapper.RetornaSelect<T>()}");
            else
            {
                sqlPesquisa.AppendLine($"USE {_nomeBanco};");
                sqlPesquisa.AppendLine(query.Trim());
            }

            return _conexao.Query<T>(sqlPesquisa.ToString(), commandTimeout: 80000000, commandType: CommandType.Text);
        }

        public IEnumerable<T> BuscarTodosPorQueryGerador<T>(string sqlWhere = null) where T : class
        {
            AbrirConexao();
            var sqlPesquisa = new StringBuilder();

            sqlPesquisa.AppendLine($"{GeradorDapper.RetornaSelect<T>()}");
            if (!string.IsNullOrEmpty(sqlWhere)) sqlPesquisa.AppendLine($"AND {sqlWhere}");

            return _conexao.Query<T>(sqlPesquisa.ToString(), commandTimeout: 80000000, commandType: CommandType.Text).ToList();
        }

        public int Excluir<T>(int id) where T : class
        {
            AbrirConexao();
            return _conexao.Execute($"{GeradorDapper.RetornaDelete<T>(id)}");
        }

        public void ExecutarComando(string comandoSql)
        {
            if (string.IsNullOrEmpty(comandoSql.Trim())) return;

            AbrirConexao();
            var comando = _conexao.CreateCommand();
            comando.Connection = _conexao;
            comando.CommandText = comandoSql.Trim();
            comando.CommandTimeout = 0;
            comando.ExecuteNonQuery();
        }

        public void ExecutarComandoDireto(CommandDefinition command)
        {
            _conexao.Execute(command);
        }

        public virtual void Dispose()
        {
            FecharConexao();
            _conexao.Dispose();
        }

        public List<T> Query<T>(string where) where T : class
        {
            return _conexao.Query<T>(where, commandTimeout: 80000000, commandType: CommandType.Text).ToList();
        }

        #endregion
    }
}
