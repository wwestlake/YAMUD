using System;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string templateName, object model);
    }
}
