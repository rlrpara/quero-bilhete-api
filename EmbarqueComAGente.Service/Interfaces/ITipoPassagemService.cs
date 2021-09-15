using QueroBilhete.Service.ViewModels;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface ITipoPassagemService : IBaseService
    {
        List<TipoPassagemViewModel> GetAll();
        TipoPassagemViewModel GetById(string id);
        bool Post(TipoPassagemViewModel TipoPassagemViewModel);
        bool Put(TipoPassagemViewModel TipoPassagemViewModel);
        bool Delete(string id);
    }
}
