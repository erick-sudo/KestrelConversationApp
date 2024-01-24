using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IJwtService
{
    GenerateTokenServiceResponse GenerateToken(GenerateTokenServiceRequest request, int expiresInMinutes);
}
