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
    public class PassagemService : BaseService, IPassagemService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly IPassagemRepository _repositorio;

        #endregion

        #region Construtor
        public PassagemService(IPassagemRepository repository, IMapper mapper)
            : base(repository)
        {
            _repositorio = repository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Públicos
        public List<PassagemViewModel> GetAll() => _mapper.Map<List<PassagemViewModel>>(_repositorio.BuscarTodosPorQueryGerador<Passagem>(""));

        public PassagemViewModel GetById(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código inválido");

            return (string.IsNullOrWhiteSpace(id) ? null : _mapper.Map<PassagemViewModel>(_repositorio.BuscarPorId<Passagem>(int.Parse(id))));
        }

        public bool Post(PassagemViewModel PassagemViewModel)
        {
            if (PassagemViewModel.Codigo == 0 && PassagemViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            Validator.ValidateObject(PassagemViewModel, new ValidationContext(PassagemViewModel), true);

            return (_repositorio.Adicionar(_mapper.Map<Passagem>(PassagemViewModel)) > 0);
        }

        public bool Put(PassagemViewModel PassagemViewModel)
        {
            if (PassagemViewModel.Codigo == 0)
                throw new ArgumentException("Código inválido");

            return (_repositorio.Atualizar(PassagemViewModel.Codigo ?? 0, _mapper.Map<Passagem>(PassagemViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<Passagem>(int.Parse(id)) > 0);
        }
        #endregion
    }
}
