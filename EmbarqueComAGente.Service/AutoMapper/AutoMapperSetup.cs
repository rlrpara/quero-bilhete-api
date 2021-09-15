using AutoMapper;
using QueroBilhete.Domain.Entities;
using QueroBilhete.Service.ViewModels;

namespace QueroBilhete.Service.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            #region ViewModelToDomain
            CreateMap<EmbarcacaoViewModel, Embarcacao>();
            CreateMap<EmpresaViewModel, Empresa>();
            CreateMap<PassagemViewModel, Passagem>();
            CreateMap<PassageiroViewModel, Passageiro>();
            CreateMap<TipoPassagemViewModel, TipoPassagem>();
            CreateMap<TrajetoViewModel, Trajeto>();
            CreateMap<UsuarioViewModel, Usuario>();
            CreateMap<ViagemViewModel, Viagem>();
            #endregion

            #region DomainToViewModel
            CreateMap<Embarcacao, EmbarcacaoViewModel>();
            CreateMap<Empresa, EmpresaViewModel>();
            CreateMap<Passagem, PassagemViewModel>();
            CreateMap<Passageiro, PassageiroViewModel>();
            CreateMap<TipoPassagem, TipoPassagemViewModel>();
            CreateMap<Trajeto, TrajetoViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<Viagem, ViagemViewModel>();
            #endregion
        }
    }
}
