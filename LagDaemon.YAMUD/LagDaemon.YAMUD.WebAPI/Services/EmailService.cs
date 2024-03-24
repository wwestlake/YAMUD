using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using FluentResults;
using LagDaemon.YAMUD.Services;
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
        private readonly SmtpSender _sender;
        private readonly RazorViewToStringRenderer _razorViewToStringRenderer;

        public EmailService(IFluentEmailFactory emailFactory, SmtpSender sender, RazorViewToStringRenderer razorViewToStringRenderer)
        {
            _emailFactory = emailFactory;
            _sender = sender;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public async Task<Result<SendResponse>> SendEmailAsync(string toEmail, string subject, string templateName, object model)
        {
            var email = _emailFactory.Create();
            email.Sender = _sender;
            email.To(toEmail)
                 .SetFrom("yamud@lagdaemon.com")
                 .Subject(subject)
                 .Body(await _razorViewToStringRenderer.RenderViewToStringAsync(templateName, model), true);
            return Result.Ok(await email.SendAsync());
        }
    }
}
