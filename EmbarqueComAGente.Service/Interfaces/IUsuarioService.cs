using QueroBilhete.Service.ViewModels;
using System.Collections.Generic;

namespace QueroBilhete.Service.Interfaces
{
    public interface IUsuarioService : IBaseService
    {
        List<UsuarioViewModel> GetAll();
        UsuarioViewModel GetById(string id);
        bool Post(UsuarioViewModel usuarioViewModel);
        bool Put(UsuarioViewModel usuarioViewModel);
        bool Delete(string id);
        //UserAuthenticateResponseViewModel Authenticate(UserAuthenticateRequestViewModel usuario);
        UsuarioViewModel AuthenticateFirebaseCheck(FirebaseAuthenticate entidade);
        bool ValidaUidUsuario(string Uid);
    }
}
