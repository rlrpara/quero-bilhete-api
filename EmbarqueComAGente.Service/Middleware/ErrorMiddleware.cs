using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QueroBilhete.Infra.Utilities.Utilitarios;
using QueroBilhete.Service.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QueroBilhete.Service.Middleware
{
    public class ErrorMiddleware
    {
        #region Propriedades Privadas
        private readonly RequestDelegate next;
        #endregion

        #region Métodos Privados
        private GerenciadorEmail.Email ObterDadosEnvio(string conteudo) => new GerenciadorEmail.Email()
        {
            HostSmtp = "mail.questores.com.br",
            AtivarSsl = true,
            EmailNome = "Rodrigo Ribeiro",
            EmailEnvio = "rodrigo.ribeiro@questores.com.br",
            SenhaEnvio = "zU6Zzoun",
            EmailDestino = "rlr.para@gmail.com; tiagobuchanelli@gmail.com",
            NomeEmailDestino = "Usuário",
            Assunto = "Teste de envio de Email",
            Conteudo = ObterMensagemLog(conteudo).Replace("\r\n", "<br/>"),
            SiteEnvio = "www.exemplo.com"
        };
        private string ObterMensagemLog(string conteudo)
        {
            var mensagem = new StringBuilder();

            mensagem.AppendLine($"Mensagem do site: x0{Environment.NewLine}{Environment.NewLine}");
            mensagem.AppendLine($"======= INÍCIO DO CONTEÚDO DE TESTES ======= ");
            mensagem.AppendLine($"Data/Hora: {DateTime.Now}.");
            mensagem.AppendLine($"Mensagem : Log para envio de Erros do sistema.");
            mensagem.AppendLine($"{conteudo}");
            mensagem.AppendLine($"======== FIM DO CONTEÚDO DE TESTES ========= ");

            return mensagem.ToString();
        }
        private void GravarLog(Exception ex)
        {
            var diretorio = Path.Combine(AppContext.BaseDirectory, "erroLogs");

            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);

            using (StreamWriter dados = new StreamWriter(Path.Combine(diretorio, "erroLogs.txt")))
            {
                dados.WriteLine($"{ex.Message} {ex?.InnerException?.Message}");
                dados.WriteLine($"{ex.StackTrace}");
            }
        }
        private Task HandleExceptionAsysnc(HttpContext context, Exception ex)
        {
            var errorReponseViewModel = new ErrorReposneViewModel(HttpStatusCode.BadRequest.ToString(), $"{ex.Message} {ex?.InnerException?.Message}");

            GravarLog(ex);
            //GerenciadorEmail.EnviarEmail(ObterDadosEnvio(ex.ToString()));

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorReponseViewModel));
        }
        #endregion

        #region Métodos Públicos
        public ErrorMiddleware(RequestDelegate context)
            => this.next = context;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
               await HandleExceptionAsysnc(context, ex);
            }
        }
        #endregion
    }
}
