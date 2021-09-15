using System;
using System.Security.Cryptography;
using System.Text;

namespace QueroBilhete.Service.Criptografia
{
    public class CriptografiaHash
    {
        private HashAlgorithm _algoritimo;

        public CriptografiaHash(HashAlgorithm algoritimo)
        {
            _algoritimo = algoritimo;
        }

        public string CriptografarSenha(string senha)
        {
            var valorCodificado = Encoding.UTF8.GetBytes(senha);
            var senhaEncriptada = _algoritimo.ComputeHash(valorCodificado);

            var lista = new StringBuilder();

            foreach (var carecter in senhaEncriptada)
                lista.Append(carecter.ToString("X2"));

            return lista.ToString();
        }

        public bool VerificarSenha(string senhaInformada, string senhaCadastrada)
        {
            if (string.IsNullOrEmpty(senhaInformada))
                throw new NullReferenceException("Cadastre uma senha.");
            
            var valorCodificado = Encoding.UTF8.GetBytes(senhaInformada);

            var senhaEncriptada = _algoritimo.ComputeHash(valorCodificado);

            var lista = new StringBuilder();

            foreach (var caracter in senhaEncriptada)
                lista.Append(caracter.ToString("X2"));

            return lista.ToString() == senhaCadastrada;
        }
    }
}
