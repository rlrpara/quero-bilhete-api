using QueroBilhete.Service.ViewModels;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface IEmbarcacaoService : IBaseService
    {
        List<EmbarcacaoViewModel> GetAll();
        EmbarcacaoViewModel GetById(string id);
        bool Post(EmbarcacaoViewModel embarcacaoViewModel);
        bool Put(EmbarcacaoViewModel embarcacaoViewModel);
        bool Delete(string id);
    }
}
