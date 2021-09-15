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
    public class PassageiroService : BaseService, IPassageiroService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly IPassageiroRepository _repositorio;

        #endregion

        #region Construtor
        public PassageiroService(IPassageiroRepository repository, IMapper mapper)
            : base(repository)
        {
            _repositorio = repository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Públicos
        public List<PassageiroViewModel> GetAll() => _mapper.Map<List<PassageiroViewModel>>(_repositorio.BuscarTodosPorQueryGerador<Passageiro>(""));

        public PassageiroViewModel GetById(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código inválido");

            return (string.IsNullOrWhiteSpace(id) ? null : _mapper.Map<PassageiroViewModel>(_repositorio.BuscarPorId<Passageiro>(int.Parse(id))));
        }

        public bool Post(PassageiroViewModel passageiroViewModel)
        {
            if (passageiroViewModel.Codigo == 0 && passageiroViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            Validator.ValidateObject(passageiroViewModel, new ValidationContext(passageiroViewModel), true);

            return (_repositorio.Adicionar(_mapper.Map<Passageiro>(passageiroViewModel)) > 0);
        }

        public bool Put(PassageiroViewModel PassageiroViewModel)
        {
            if (PassageiroViewModel.Codigo == 0)
                throw new ArgumentException("Código inválido");

            return (_repositorio.Atualizar(PassageiroViewModel.Codigo ?? 0, _mapper.Map<Passageiro>(PassageiroViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<Passageiro>(int.Parse(id)) > 0);
        }
        #endregion
    }
}
