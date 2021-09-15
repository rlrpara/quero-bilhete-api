using QueroBilhete.Domain.Entities;
using QueroBilhete.Service.ViewModels;
using QueroBilhete.Service.ViewModels.Consulta;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface IViagemService : IBaseService
    {
        List<ViagemViewModel> GetAll();
        ViagemViewModel GetById(string id);
        bool Post(ViagemViewModel ViajemViewModel);
        List<DadosConsultaViagem> PostConsultaViagem(ConsultaViagemOrigemDestino consultaViagemOrigemDestino);
        bool Put(ViagemViewModel ViajemViewModel);
        bool Delete(string id);
    }
}
