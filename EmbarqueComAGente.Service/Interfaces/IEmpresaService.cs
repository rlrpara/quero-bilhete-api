using QueroBilhete.Service.ViewModels;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface IEmpresaService : IBaseService
    {
        List<EmpresaViewModel> GetAll();
        EmpresaViewModel GetById(string id);
        bool Post(EmpresaViewModel empresaViewModel);
        bool Put(EmpresaViewModel empresaViewModel);
        bool Delete(string id);
    }
}
