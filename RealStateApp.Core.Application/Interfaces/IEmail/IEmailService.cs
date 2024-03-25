using RealStateApp.Core.Application.Dto.Email;

namespace RealStateApp.Core.Application.Interfaces.IEmail;

public interface IEmailService
{
    Task SendAsync(EmailRequest request);
}