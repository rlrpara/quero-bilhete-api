using Dapper;
using QueroBilhete.Domain.interfaces;
using QueroBilhete.Service.Interfaces;
using System.Collections.Generic;

namespace QueroBilhete.Service.Service
{
    public class BaseService : IBaseService
    {
        public readonly IBaseRepository _baseRepository;

        public BaseService(IBaseRepository repositorio)
        {
            _baseRepository = repositorio;
        }
        public int Adicionar<T>(T entidade) where T : class
        {
            try
            {
                return _baseRepository.Adicionar(entidade);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public int Atualizar<T>(int id, T entidade) where T : class
        {
            try
            {
                return _baseRepository.Atualizar(id, entidade);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public T BuscarPorId<T>(int id) where T : class
        {
            try
            {
                return _baseRepository.BuscarPorId<T>(id);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public T BuscarPorQuery<T>(string query)
        {
            try
            {
                return _baseRepository.BuscarPorQuery<T>(query);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public IEnumerable<T> BuscarTodosPorQuery<T>(string query = "") where T : class
        {
            try
            {
                return _baseRepository.BuscarTodosPorQuery<T>(query);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public IEnumerable<T> BuscarTodosPorQueryGerador<T>(string sqlWhere = "") where T : class
        {
            try
            {
                return _baseRepository.BuscarTodosPorQueryGerador<T>(sqlWhere);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public int Excluir<T>(int id) where T : class
        {
            try
            {
                return _baseRepository.Excluir<T>(id);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public void ExecutarComando(string comandoSql)
        {
            _baseRepository.ExecutarComando(comandoSql);
        }

        public void ExecutarComandoDireto(CommandDefinition command)
        {
            _baseRepository.ExecutarComandoDireto(command);
        }

        public List<T> Query<T>(string where) where T : class
        {
            return _baseRepository.Query<T>(where);
        }
    }
}
