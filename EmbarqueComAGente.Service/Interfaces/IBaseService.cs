using Dapper;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface IBaseService
    {
        List<T> Query<T>(string where) where T : class;
        T BuscarPorId<T>(int id) where T : class;
        T BuscarPorQuery<T>(string query);
        IEnumerable<T> BuscarTodosPorQuery<T>(string query = "") where T : class;
        IEnumerable<T> BuscarTodosPorQueryGerador<T>(string sqlWhere = "") where T : class;
        int Adicionar<T>(T entidade) where T : class;
        int Atualizar<T>(int id, T entidade) where T : class;
        int Excluir<T>(int id) where T : class;
        void ExecutarComando(string comandoSql);
        void ExecutarComandoDireto(CommandDefinition command);
    }
}
