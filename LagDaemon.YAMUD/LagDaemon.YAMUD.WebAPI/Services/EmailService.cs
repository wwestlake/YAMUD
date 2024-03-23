using FluentEmail.Core;
using FluentEmail.Razor;
using LagDaemon.YAMUD.WebAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmailFactory _emailFactory;
        private readonly IWebHostEnvironment _env;

        public EmailService(IFluentEmailFactory emailFactory, IWebHostEnvironment env)
        {
            _emailFactory = emailFactory;
            _env = env;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string templateName, object model)
        {
            var email = _emailFactory.Create();

            email.To(toEmail)
                 .Subject(subject)
                 .UsingTemplate(await GetEmailTemplate(templateName), model, isHtml: true);

            await email.SendAsync();
        }

        private async Task<string> GetEmailTemplate(string templateName)
        {
            string templatePath = Path.Combine(_env.ContentRootPath, "EmailTemplates", $"{templateName}.cshtml");

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template '{templateName}' not found.");
            }

            return await File.ReadAllTextAsync(templatePath);
        }
    }
}
