using AutoMapper;
using QueroBilhete.Domain.Entities;
using QueroBilhete.Domain.interfaces;
using QueroBilhete.Infra.Utilities.ExtensionMethods;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using QueroBilhete.Service.ViewModels.Consulta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace QueroBilhete.Service.Service
{
    public class ViagemService : BaseService, IViagemService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly IViagemRepository _repositorio;

        #endregion

        #region Construtor
        public ViagemService(IViagemRepository repository, IMapper mapper) : base(repository)
        {
            _repositorio = repository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Públicos
        public List<ViagemViewModel> GetAll() => _mapper.Map<List<ViagemViewModel>>(_repositorio.BuscarTodosPorQueryGerador<Viagem>(""));

        public ViagemViewModel GetById(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código inválido");

            return (string.IsNullOrWhiteSpace(id) ? null : _mapper.Map<ViagemViewModel>(_repositorio.BuscarPorId<Viagem>(int.Parse(id))));
        }

        public bool Post(ViagemViewModel viagemViewModel)
        {
            if (viagemViewModel.Codigo != 0 && viagemViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            Validator.ValidateObject(viagemViewModel, new ValidationContext(viagemViewModel), true);

            return (_repositorio.Adicionar(_mapper.Map<Viagem>(viagemViewModel)) > 0);
        }

        public List<DadosConsultaViagem> PostConsultaViagem(ConsultaViagemOrigemDestino consulta)
        {
            var dataIda = consulta.DataIda;
            var dataVolta = consulta.DataVolta;
            var origem = consulta.Origem;
            var destino = consulta.Destino;

            return _repositorio.PostConsultaViagem(dataIda, dataVolta, origem, destino, consulta.PaginaAtual, consulta.QuantidadePorPagina);
        }

        public bool Put(ViagemViewModel viagemViewModel)
        {
            if (viagemViewModel.Codigo == 0)
                throw new ArgumentException("Código inválido");

            Validator.ValidateObject(viagemViewModel, new ValidationContext(viagemViewModel), true);

            return (_repositorio.Atualizar(viagemViewModel.Codigo ?? 0, _mapper.Map<Viagem>(viagemViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<Viagem>(int.Parse(id)) > 0);
        }
        #endregion
    }
}
