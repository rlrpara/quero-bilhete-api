using Dapper;
using System.Collections.Generic;

namespace QueroBilhete.Domain.interfaces
{
    public interface IBaseRepository
    {
        List<T> Query<T>(string where) where T : class;
        T BuscarPorId<T>(int id) where T : class;
        T BuscarPorQuery<T>(string query);
        T BuscarPorQueryGerador<T>(string sqlWhere = null) where T : class;
        IEnumerable<T> BuscarTodosPorQuery<T>(string query = null) where T : class;
        IEnumerable<T> BuscarTodosPorQueryGerador<T>(string sqlWhere = null) where T : class;
        int Adicionar<T>(T entidade) where T : class;
        int Atualizar<T>(int id, T entidade) where T : class;
        int Excluir<T>(int id) where T : class;
        void ExecutarComando(string comandoSql);
        void ExecutarComandoDireto(CommandDefinition command);
    }
}
