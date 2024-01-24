using Core.DTOs.Identity;
using Core.Interfaces.Services;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.Services;

public class EmailService(IOptions<SmtpSettings> smtpSettings) : IEmailService
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

    public async Task<SendResponse> SendCreatePasswordEmailAsync(SendEmailServiceRequest request, CancellationToken cancellationToken)
    {
        var subject = _smtpSettings.CreateNewPasswordSubject;
        return await SendEmailAsync(request, subject, cancellationToken);
    }

    public async Task<SendResponse> SendPasswordResetEmailAsync(SendEmailServiceRequest request, CancellationToken cancellationToken)
    {
        var subject = _smtpSettings.ResetPasswordSubject;
        return await SendEmailAsync(request, subject, cancellationToken);
    }

    private async Task<SendResponse> SendEmailAsync(SendEmailServiceRequest request, string subject, CancellationToken cancellationToken)
    {
        string template = GetAppropriateTemplate(request, subject);

        var sender = new SmtpSender(() => new SmtpClient()
        {
            Host = _smtpSettings.Host, 
            Port = _smtpSettings.Port,
            EnableSsl = _smtpSettings.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
        });

        Email.DefaultSender = sender;
        Email.DefaultRenderer = new RazorRenderer();

        try
        {
            return await Email
               .From(emailAddress: _smtpSettings.FromEmail)
               .To(emailAddress: request.ToEmail, name: request.UserName)
               .Subject(subject: subject)
               .UsingTemplate(template, new { request.UserName })
               .SendAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return SendErrorMessages(e);
        }
    }

    #region Helper Methods
    private string GetAppropriateTemplate(SendEmailServiceRequest request, string subject)
    {
        var template = string.Empty;

        if (subject == _smtpSettings.CreateNewPasswordSubject)
        {
            template = GetCreateNewPasswordTemplate(request.Link);
        }
        else if (subject == _smtpSettings.ResetPasswordSubject)
        {
            template = GetPasswordResetTemplate(request.Link);
        }

        return template;
    }

    private static string GetPasswordResetTemplate(string link)
    {
        StringBuilder template = new();
        template.AppendLine("<div style='border: 1px solid #ccc; width: 500px; text-align: center; padding: 10px;'>");
        template.AppendLine("<h2>Dear @Model.UserName,</h2>");
        template.AppendLine("<h4>Click the button below to create your new password:</h4>");
        template.AppendLine($"<a href='{link}' style='display: inline-block; padding: 10px 20px; background-color: orange; text-decoration: none; color: white;'>Click Here</a>");
        template.AppendLine("<p>ConversationApp Team</p>");
        template.AppendLine("<div style='border: 1px solid #ccc; padding: 10px; margin-top: 10px; word-wrap: break-word;'>");
        template.AppendLine($"<strong>Link:</strong><br>{link}");
        template.AppendLine("</div>");
        template.AppendLine("</div>");

        return template.ToString();
    }

    private static string GetCreateNewPasswordTemplate(string link)
    {
        var createPasswordButtonTemplate = GetPasswordResetTemplate(link);

        StringBuilder template = new();
        template.AppendLine($"<h2>Welcome to Conversation App, @Model.UserName!</h2>");
        template.AppendLine(createPasswordButtonTemplate);

        return template.ToString();
    }

    private static SendResponse SendErrorMessages(Exception e)
    {
        var response = new SendResponse();
        response.ErrorMessages.Add(e.Message);

        if (e.InnerException != null)
        {
            response.ErrorMessages.Add(e.InnerException.Message);
        }

        return response;
    }
    #endregion

}
