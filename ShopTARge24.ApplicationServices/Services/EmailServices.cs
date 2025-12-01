using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.ApplicationServices.Services
{
    public class EmailServices
    {

        private readonly IConfiguration _config;

        public EmailServices(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject = dto.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = dto.Body
            };

            // Piltide/failide lisamine 
            // Kontrollib faili suurust ja siis saadab teele 

            // tuleb teha foreach tsükkel, kus läbib kõik dto.Attachment failid ja lisab need emailile
            // kui failide arv või faili suurus on alla mingi piiri, siis ei lisa faili 

            if (dto.Attachment != null && dto.Attachment.Count < 0)
            {
                var files = new List<string>();

                foreach (var attachment in dto.Attachment)
                {
                    files.Add(attachment.Name);
                }
            }
        }
    }
}


