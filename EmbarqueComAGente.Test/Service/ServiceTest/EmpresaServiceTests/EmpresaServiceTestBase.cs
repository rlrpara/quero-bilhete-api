using QueroBilhete.Domain.Entities;
using QueroBilhete.Service.ViewModels;
using System;
using System.Collections.Generic;

namespace QueroBilhete.Test.Service.ServiceTest.EmpresaServiceTests
{
    public class EmpresaServiceTestBase
    {
        public EmpresaViewModel ObterNovaEmpresa()
        {
            return new EmpresaViewModel
            {
                RazaoSocial = "Empresa teste",
                Cnpj = "06216168000115",
                InscricaoEstadual = "",
                InscricaoMunicipal = "",
                Telefone = "49988020959",
                Email = "rlr.para@gmail.com",
                Site = "www.empresa.com.br",
                Logo = "",
                Ativo = true,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            };
        }

        public List<Empresa> ObterListaEmpresas()
        {
            return new List<Empresa>
            {
                new Empresa
                {
                    Codigo = 1,
                    RazaoSocial = "Empresa teste",
                    Cnpj = "06216168000115",
                    InscricaoEstadual = "",
                    InscricaoMunicipal = "",
                    Telefone = "49988020959",
                    Celular = "49988020959",
                    Email = "rlr.para@gmail.com",
                    Site = "www.empresa.com.br",
                    Logo = "",
                    Ativo = true,
                    DataCadastro = DateTime.Now,
                    DataAtualizacao = DateTime.Now
                },
                new Empresa
                {
                    Codigo = 2,
                    RazaoSocial = "Empresa teste2",
                    Cnpj = "2222222222222",
                    InscricaoEstadual = "2",
                    InscricaoMunicipal = "2",
                    Telefone = "49988020959",
                    Celular = "49988020959",
                    Email = "rlr.para@gmail.com",
                    Site = "www.empresa.com.br",
                    Logo = "",
                    Ativo = true,
                    DataCadastro = DateTime.Now,
                    DataAtualizacao = DateTime.Now
                }
            };
        }

        public Empresa ObterEmpresa1() => new Empresa()
        {
            Codigo = 1,
            RazaoSocial = "Empresa teste",
            Cnpj = "06216168000115",
            InscricaoEstadual = "",
            InscricaoMunicipal = "",
            Telefone = "49988020959",
            Celular = "49988020959",
            Email = "rlr.para@gmail.com",
            Site = "www.empresa.com.br",
            Logo = "",
            Ativo = true,
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };

        public EmpresaViewModel ObterNovaEmpresaIncompleta() => new EmpresaViewModel()
        {
            RazaoSocial = "Empresa teste"
        };
    }
}
