using FluentEmail.Core.Models;
using FluentResults;

namespace LagDaemon.YAMUD.WebAPI.Services;

public interface IEmailService
{
    Task<Result<SendResponse>> SendEmailAsync(string toEmail, string subject, string templateName, object model);
}
