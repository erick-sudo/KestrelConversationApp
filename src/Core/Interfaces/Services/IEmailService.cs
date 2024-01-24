using Core.DTOs.Identity;
using FluentEmail.Core.Models;

namespace Core.Interfaces.Services;

public interface IEmailService
{
    Task<SendResponse> SendCreatePasswordEmailAsync(SendEmailServiceRequest request, CancellationToken cancellationToken);
    Task<SendResponse> SendPasswordResetEmailAsync(SendEmailServiceRequest request, CancellationToken cancellationToken);
}
