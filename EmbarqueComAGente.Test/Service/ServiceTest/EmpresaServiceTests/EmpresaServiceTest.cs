using AutoMapper;
using QueroBilhete.Domain.Entities;
using QueroBilhete.Domain.interfaces;
using QueroBilhete.Service.AutoMapper;
using QueroBilhete.Service.Service;
using QueroBilhete.Service.ViewModels;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace QueroBilhete.Test.Service.ServiceTest.EmpresaServiceTests
{
    [Trait("Service", "EmpresaService")]
    public class EmpresaServiceTest : EmpresaServiceTestBase
    {
        #region Propriedades Privadas
        private EmpresaService _EmpresaService;

        #endregion

        #region Construtor
        public EmpresaServiceTest()
        {
            var moqEmpresaRepositorio = new Mock<IEmpresaRepository>().Object;
            var moqMapper = new Mock<IMapper>().Object;
            _EmpresaService = new EmpresaService(moqEmpresaRepositorio, moqMapper);
        }

        #endregion

        #region Métodos Privados
        private Mapper ObterMapperConfig()
        {
            var autoMapperProfile = new AutoMapperSetup();
            var configuration = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
            return new Mapper(configuration);
        }
        #endregion

        #region Valida Codigo Obrigatório
        [Fact(DisplayName = "Deve invalidar ao enviar Id via método Post")]
        public void DeveInvalidarEnviarIDViaMetodoPost()
        {
            var exception = Assert.Throws<ArgumentException>(() => _EmpresaService.Post(new EmpresaViewModel { Codigo = 1 }));

            Assert.Equal("O Código deve ser nulo", exception.Message);
        }

        [Fact(DisplayName = "Deve invalidar ao enviar id vazia ou nulla via GetById")]
        public void DeveInvalidarEnviarIDVaziaNUlaViaGetById()
        {
            Exception exception = Assert.Throws<ArgumentException>(() => _EmpresaService.GetById(""));

            Assert.Equal("Código inválido", exception.Message);
        }

        [Fact(DisplayName = "Deve invalidar ao enviar id vazia ou nulla via Put")]
        public void DeveInvalidarEnviarIDVaziaNulaViaPut()
        {
            Exception exception = Assert.Throws<ValidationException>(() => _EmpresaService.Put(new EmpresaViewModel()));

            Assert.Equal("Campo obrigatório", exception.Message);
        }

        [Fact(DisplayName = "Deve invalidar ao enviar id vazia ou nulla via Delete")]
        public void DeveInvalidarEnviarIDVaziaNulaViaDelete()
        {
            Exception exception = Assert.Throws<ArgumentException>(() => _EmpresaService.Delete(""));

            Assert.Equal("Código não informado.", exception.Message);
        }

        //[Fact(DisplayName = "Deve invalidar ao enviar dados da autenticação vazia")]
        //public void DeveInvalidarEnviarDadosAutenticacaoVazia()
        //{
        //    Exception exception = Assert.Throws<ArgumentException>(() => _EmpresaService.Authenticate(new UserAuthenticateRequestViewModel()));

        //    Assert.Equal("Email/Senha são necessários", exception.Message);
        //}

        #endregion

        #region Valida Objetos Corretos
        [Fact(DisplayName = "Deve enviar um objeto válido via Post")]
        public void DeveEnviarUmObjetoValidoViaPost()
        {
            Assert.False(_EmpresaService.Post(ObterNovaEmpresa()));
        }

        [Fact(DisplayName = "Deve retornar uma lista maior que 0 via GetAll")]
        public void DeveRetornarListaMaiorQueZeroViaGetAll()
        {
            var EmpresaRepository = new Mock<IEmpresaRepository>();

            EmpresaRepository.Setup(x => x.BuscarTodosPorQueryGerador<Empresa>("")).Returns(ObterListaEmpresas());

            var resultado = new EmpresaService(EmpresaRepository.Object, ObterMapperConfig()).GetAll();

            Assert.True(resultado.Count > 0);
        }

        [Fact(DisplayName = "Deve retornar um Empresa quando informado seu código válido via GetById")]
        public void DeveRetornarUmEmpresaQuandoInformadoSeuCodigoValidoViaGetById()
        {
            var EmpresaRepository = new Mock<IEmpresaRepository>();

            EmpresaRepository.Setup(x => x.BuscarPorId<Empresa>(1)).Returns(ObterEmpresa1());

            var Empresa = new EmpresaService(EmpresaRepository.Object, ObterMapperConfig()).GetById("1");

            Assert.Equal("EMPRESA TESTE", Empresa.RazaoSocial);
        }

        #endregion

        #region Valida Campos Obrigatórios
        [Fact(DisplayName = "Deve invalidar quando não enviar um campo obrigatório via Post")]
        public void DeveInvalidaQuandoNaoEnviaCampoObrigatorioViaPost()
        {
            Assert.Equal("Campo obrigatório", Assert.Throws<ValidationException>(() => _EmpresaService.Post(ObterNovaEmpresaIncompleta())).Message);
        }

        [Fact(DisplayName = "Deve validar quando o período for instânciado.")]
        public void DeveValidarQuandoServicoForInstanciado()
        {
            var EmpresaMock = new Mock<IEmpresaRepository>();

            var EmpresaRepository = new EmpresaService(EmpresaMock.Object, ObterMapperConfig());

            Assert.True(EmpresaRepository != null);
        }

        #endregion
    }
}
