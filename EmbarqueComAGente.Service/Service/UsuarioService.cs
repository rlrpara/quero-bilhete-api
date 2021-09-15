using AutoMapper;
using QueroBilhete.Domain.Entities;
using QueroBilhete.Domain.interfaces;
using QueroBilhete.Infra.Utilities.ExtensionMethods;
using QueroBilhete.Service.Criptografia;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace QueroBilhete.Service.Service
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repositorio;
        private readonly CriptografiaHash _cripto = new CriptografiaHash(HashAlgorithm.Create("SHA-256"));
        private readonly string _segredo = Environment.GetEnvironmentVariable("Secret");

        #endregion

        #region Construtor
        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
            : base(repository)
        {
            _repositorio = repository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Privados
        private string ObterCriptografia(UsuarioViewModel entidade)
            => _cripto.CriptografarSenha($"{entidade.Senha}{_segredo}");
        private bool ValidarSenha(UserAuthenticateRequestViewModel entidade, Usuario usuario)
            => _cripto.VerificarSenha($"{entidade.Senha}{_segredo}", usuario.Senha);
        private UsuarioViewModel ObterNovoUsuarioViewModel(FirebaseAuthenticate entidade) => new UsuarioViewModel()
        {
            Uid = entidade.Uid,
            Nome = entidade.NomeUsuario,
            Email = entidade.Email,
            CodigoNivelAcesso = 1,
            Ativo = true,
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };
        #endregion

        #region Métodos Públicos
        public List<UsuarioViewModel> GetAll() => _mapper.Map<List<UsuarioViewModel>>(_repositorio.BuscarTodosPorQueryGerador<Usuario>(""));
        public UsuarioViewModel GetById(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código inválido");

            return (string.IsNullOrWhiteSpace(id) ? null : _mapper.Map<UsuarioViewModel>(_repositorio.BuscarPorId<Usuario>(int.Parse(id))));
        }
        public bool Post(UsuarioViewModel usuarioViewModel)
        {
            if (usuarioViewModel.Codigo != 0 && usuarioViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            if(!string.IsNullOrEmpty(usuarioViewModel.Senha))
                usuarioViewModel.Senha = ObterCriptografia(usuarioViewModel);

            Validator.ValidateObject(usuarioViewModel, new ValidationContext(usuarioViewModel), true);

            return (_repositorio.Adicionar(_mapper.Map<Usuario>(usuarioViewModel)) > 0);
        }
        public bool Put(UsuarioViewModel usuarioViewModel)
        {
            if (usuarioViewModel.Codigo == 0)
                throw new ArgumentException("Código inválido");

            if (!string.IsNullOrEmpty(usuarioViewModel.Senha))
                usuarioViewModel.Senha = ObterCriptografia(usuarioViewModel);

            return (_repositorio.Atualizar(usuarioViewModel.Codigo ?? 0, _mapper.Map<Usuario>(usuarioViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<Usuario>(int.Parse(id)) > 0);
        }
        //public UserAuthenticateResponseViewModel Authenticate(UserAuthenticateRequestViewModel entidade)
        //{
        //    if (string.IsNullOrEmpty(entidade.Email) || string.IsNullOrEmpty(entidade.Senha))
        //        throw new ArgumentException("Email/Senha são necessários");

        //    var usuario = _repositorio.BuscarTodosPorQueryGerador<Usuario>($"EMAIL = '{entidade.Email.ToLower()}'").FirstOrDefault();

        //    if(!ValidarSenha(entidade, usuario))
        //        throw new ArgumentException("Email/Senha inválidos");

        //    return (usuario == null ? null : new UserAuthenticateResponseViewModel(_mapper.Map<UsuarioViewModel>(usuario), TokenService.GenerateToken(usuario)));

        //}
        public UsuarioViewModel AuthenticateFirebaseCheck(FirebaseAuthenticate entidade)
        {
            if (string.IsNullOrEmpty(entidade.Uid))
                throw new ArgumentException("Entidade inválida");

            UsuarioViewModel usuarioModel = _mapper.Map<UsuarioViewModel>(_baseRepository.BuscarPorQueryGerador<Usuario>($"UID = '{entidade.Uid}'"));
            
            if(usuarioModel == null && Post(ObterNovoUsuarioViewModel(entidade)))
            {
                usuarioModel = _mapper.Map<UsuarioViewModel>(_baseRepository.BuscarPorQueryGerador<Usuario>($"UID = '{entidade.Uid}'"));
                usuarioModel.NovoUsuario = true;
            }

            return usuarioModel;
        }

        public bool ValidaUidUsuario(string Uid)
        {
            if (string.IsNullOrEmpty(Uid))
                throw new ArgumentException("Entidade inválida");

            return _baseRepository.BuscarPorQueryGerador<Usuario>($"UID = '{Uid}'") != null;
        }
        #endregion
    }
}
