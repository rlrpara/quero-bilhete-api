using QueroBilhete.Service.ViewModels;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface IPassageiroService : IBaseService
    {
        List<PassageiroViewModel> GetAll();
        PassageiroViewModel GetById(string id);
        bool Post(PassageiroViewModel PassageiroViewModel);
        bool Put(PassageiroViewModel PassageiroViewModel);
        bool Delete(string id);
    }
}
