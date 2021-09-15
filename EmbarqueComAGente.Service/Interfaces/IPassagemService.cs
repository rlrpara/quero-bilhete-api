using QueroBilhete.Service.ViewModels;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface IPassagemService : IBaseService
    {
        List<PassagemViewModel> GetAll();
        PassagemViewModel GetById(string id);
        bool Post(PassagemViewModel PassagemViewModel);
        bool Put(PassagemViewModel PassagemViewModel);
        bool Delete(string id);
    }
}
