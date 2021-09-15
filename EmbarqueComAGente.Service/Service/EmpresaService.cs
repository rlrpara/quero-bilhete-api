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
    public class EmpresaService : BaseService, IEmpresaService
    {
        #region Propriedades Privadas
        private readonly IMapper _mapper;
        private readonly IEmpresaRepository _repositorio;

        #endregion

        #region Construtor
        public EmpresaService(IEmpresaRepository repository, IMapper mapper)
            : base(repository)
        {
            _repositorio = repository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Públicos
        public List<EmpresaViewModel> GetAll() => _mapper.Map<List<EmpresaViewModel>>(_repositorio.BuscarTodosPorQueryGerador<Empresa>("").ToList());

        public EmpresaViewModel GetById(string id)
        {
            if (!id.IsNumeric() || string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Código inválido");

            return (_repositorio.BuscarPorId<Empresa>(int.Parse(id)) == null ? null : _mapper.Map<EmpresaViewModel>(_repositorio.BuscarPorId<Empresa>(int.Parse(id))));
        }

        public bool Post(EmpresaViewModel empresaViewModel)
        {
            if (empresaViewModel.Codigo != 0 && empresaViewModel.Codigo != null)
                throw new ArgumentException("O Código deve ser nulo");

            Validator.ValidateObject(empresaViewModel, new ValidationContext(empresaViewModel), true);

            Empresa teste = _mapper.Map<Empresa>(empresaViewModel);

            return (_repositorio.Adicionar(teste) > 0);
        }

        public bool Put(EmpresaViewModel empresaViewModel)
        {
            if (empresaViewModel.Codigo == 0 && empresaViewModel.Codigo == null)
                throw new ArgumentException("Código inválido");

            Validator.ValidateObject(empresaViewModel, new ValidationContext(empresaViewModel), true);

            return (_repositorio.Atualizar(empresaViewModel.Codigo ?? 0, _mapper.Map<Empresa>(empresaViewModel)) > 0);
        }
        public bool Delete(string id)
        {
            if (!id.IsNumeric())
                throw new ArgumentException("Código não informado.");

            return (_repositorio.Excluir<Empresa>(int.Parse(id)) > 0);
        }
        #endregion
    }
}
