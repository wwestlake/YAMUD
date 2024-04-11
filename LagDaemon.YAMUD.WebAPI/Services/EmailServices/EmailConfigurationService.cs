using FluentEmail.Smtp;

namespace LagDaemon.YAMUD.WebAPI.Services.EmailServices;

public class EmailConfigurationService
{
    private readonly IConfiguration _configuration;

    public EmailConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SmtpSender ConfigureSmtpSender()
    {
        var smtpHost = _configuration["SmtpSettings:Host"];
        var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
        var smtpUsername = _configuration["SmtpSettings:Username"];
        var smtpPassword = _configuration["SmtpSettings:Password"];
        var smtpEnableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

        return new SmtpSender(() => new System.Net.Mail.SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = smtpEnableSsl
        });
    }
}
