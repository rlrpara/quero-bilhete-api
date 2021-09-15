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
    public class TrajetoService : BaseService, ITrajetoService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly ITrajetoRepository _repositorio;

        #endregion

        #region Construtor
        public TrajetoService(ITrajetoRepository repository, IMapper mapper)
            : base(repository)
        {
            _repositorio = repository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Públicos
        public List<TrajetoViewModel> GetAll() => _mapper.Map<List<TrajetoViewModel>>(_repositorio.BuscarTodosPorQueryGerador<Trajeto>(""));

        public TrajetoViewModel GetById(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código inválido");

            return (string.IsNullOrWhiteSpace(id) ? null : _mapper.Map<TrajetoViewModel>(_repositorio.BuscarPorId<Trajeto>(int.Parse(id))));
        }

        public bool Post(TrajetoViewModel trajetoViewModel)
        {
            if (trajetoViewModel.Codigo != 0 && trajetoViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            if(trajetoViewModel.CodigoEmbarcacao == 0)
                throw new ArgumentException("O Código da Embarcação não pode ser 0.");

            Validator.ValidateObject(trajetoViewModel, new ValidationContext(trajetoViewModel), true);

            return (_repositorio.Adicionar(_mapper.Map<Trajeto>(trajetoViewModel)) > 0);
        }

        public bool Put(TrajetoViewModel TrajetoViewModel)
        {
            if (TrajetoViewModel.Codigo == 0)
                throw new ArgumentException("Código inválido");

            return (_repositorio.Atualizar(TrajetoViewModel.Codigo ?? 0, _mapper.Map<Trajeto>(TrajetoViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<Trajeto>(int.Parse(id)) > 0);
        }
        #endregion
    }
}
