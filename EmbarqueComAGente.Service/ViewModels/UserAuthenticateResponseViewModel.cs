namespace QueroBilhete.Service.ViewModels
{
    public class UserAuthenticateResponseViewModel
    {
        public UsuarioViewModel Usuario { get; set; }
        public string Token { get; set; }

        public UserAuthenticateResponseViewModel(UsuarioViewModel usuarioViewModel, string token)
        {
            usuarioViewModel.Senha = "";

            Usuario = usuarioViewModel;
            Token = token;
        }
    }
}
