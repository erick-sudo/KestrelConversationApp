namespace Core.DTOs.Identity;

public class SendEmailServiceRequest
{
    public string UserName { get; set; }

    public string ToEmail { get; set; }

    public string Link { get; set; }
}
