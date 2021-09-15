using QueroBilhete.Service.ViewModels;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface ITrajetoService : IBaseService
    {
        List<TrajetoViewModel> GetAll();
        TrajetoViewModel GetById(string id);
        bool Post(TrajetoViewModel TrajetoViewModel);
        bool Put(TrajetoViewModel TrajetoViewModel);
        bool Delete(string id);
    }
}
