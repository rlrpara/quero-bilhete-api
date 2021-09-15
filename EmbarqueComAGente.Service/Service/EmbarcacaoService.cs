using AutoMapper;
using QueroBilhete.Domain.Entities;
using QueroBilhete.Domain.interfaces;
using QueroBilhete.Infra.Utilities.ExtensionMethods;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace QueroBilhete.Service.Service
{
    public class EmbarcacaoService : BaseService, IEmbarcacaoService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly IEmbarcacaoRepository _repositorio;

        #endregion

        #region Construtor
        public EmbarcacaoService(IEmbarcacaoRepository embarcacaoRepository, IMapper mapper)
            : base(embarcacaoRepository)
        {
            _repositorio = embarcacaoRepository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Públicos
        public List<EmbarcacaoViewModel> GetAll()
        {
            var dadosEmbarcacao = _repositorio.BuscarTodosPorQueryGerador<Embarcacao>("").ToList();

            return (dadosEmbarcacao.Count == 0 ? new List<EmbarcacaoViewModel>() : _mapper.Map<List<EmbarcacaoViewModel>>(dadosEmbarcacao));
        }

        public EmbarcacaoViewModel GetById(string id)
        {
            if (!id.IsNumeric() || string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Código inválido");

             return _repositorio.BuscarPorId<Embarcacao>(int.Parse(id)) == null ? null : _mapper.Map<EmbarcacaoViewModel>(_repositorio.BuscarPorId<Embarcacao>(int.Parse(id)));
        }

        public bool Post(EmbarcacaoViewModel embarcacaoViewModel)
        {
            if (embarcacaoViewModel.Codigo != 0 && embarcacaoViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            //verificar se existe o codigo da empresa informada

            Validator.ValidateObject(embarcacaoViewModel, new ValidationContext(embarcacaoViewModel), true);

            return (_repositorio.Adicionar(_mapper.Map<Embarcacao>(embarcacaoViewModel)) > 0);
        }

        public bool Put(EmbarcacaoViewModel embarcacaoViewModel)
        {
            if (embarcacaoViewModel.Codigo == 0)
                throw new ArgumentException("Código inválido");

            return (_repositorio.Atualizar(embarcacaoViewModel.Codigo ?? 0, _mapper.Map<Embarcacao>(embarcacaoViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<Embarcacao>(int.Parse(id)) > 0);
        }
        #endregion
    }
}
