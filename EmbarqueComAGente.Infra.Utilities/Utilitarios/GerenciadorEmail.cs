using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace QueroBilhete.Infra.Utilities.Utilitarios
{
    public static class GerenciadorEmail
    {
        public static void EnviarEmail(Email email)
        {
            var contador = 0;
            using var clientEmail = new SmtpClient()
            {
                Host = email.HostSmtp,
                Port = 587,
                EnableSsl = email.AtivarSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email.EmailEnvio, email.SenhaEnvio)
            };

            using var mail = new MailMessage
            {
                Sender = new MailAddress(email.EmailEnvio, email.EmailNome),
                From = new MailAddress(email.EmailEnvio, email.EmailNome),
                Subject = email.Assunto,
                Body = email.Conteudo.Replace("x0", email.SiteEnvio),
                IsBodyHtml = true,
                Priority = MailPriority.Normal
            };

            foreach (var item in email.EmailDestino.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToArray())
            {
                if (contador == 0)
                    mail.To.Add(item);
                else
                    mail.Bcc.Add(item);
                contador++;
            }

            clientEmail.Send(mail);
        }

        public class Email
        {
            public string HostSmtp { get; set; }
            public bool AtivarSsl { get; set; }
            public string EmailEnvio { get; set; }
            public string SenhaEnvio { get; set; }
            public string EmailNome { get; set; }
            public string EmailDestino { get; set; }
            public string NomeEmailDestino { get; set; }
            public string Assunto { get; set; }
            public string Conteudo { get; set; }
            public string SiteEnvio { get; set; }

        }
    }
}
