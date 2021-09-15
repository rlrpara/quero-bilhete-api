using AutoMapper;
using QueroBilhete.Domain.Entities;
using QueroBilhete.Domain.interfaces;
using QueroBilhete.Infra.Utilities.ExtensionMethods;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace QueroBilhete.Service.Service
{
    public class TipoPassagemService : BaseService, ITipoPassagemService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly ITipoPassagemRepository _repositorio;

        #endregion

        #region Construtor
        public TipoPassagemService(ITipoPassagemRepository repository, IMapper mapper)
            : base(repository)
        {
            _repositorio = repository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Públicos
        public List<TipoPassagemViewModel> GetAll() => _mapper.Map<List<TipoPassagemViewModel>>(_repositorio.BuscarTodosPorQueryGerador<TipoPassagem>(""));

        public TipoPassagemViewModel GetById(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código inválido");

            return (string.IsNullOrWhiteSpace(id) ? null : _mapper.Map<TipoPassagemViewModel>(_repositorio.BuscarPorId<TipoPassagem>(int.Parse(id))));
        }

        public bool Post(TipoPassagemViewModel tipoPassagemViewModel)
        {
            if (tipoPassagemViewModel.Codigo == 0 && tipoPassagemViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            Validator.ValidateObject(tipoPassagemViewModel, new ValidationContext(tipoPassagemViewModel), true);

            return (_repositorio.Adicionar(_mapper.Map<TipoPassagem>(tipoPassagemViewModel)) > 0);
        }

        public bool Put(TipoPassagemViewModel TipoPassagemViewModel)
        {
            if (TipoPassagemViewModel.Codigo == 0)
                throw new ArgumentException("Código inválido");

            return (_repositorio.Atualizar(TipoPassagemViewModel.Codigo ?? 0, _mapper.Map<TipoPassagem>(TipoPassagemViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<TipoPassagem>(int.Parse(id)) > 0);
        }
        #endregion
    }
}
