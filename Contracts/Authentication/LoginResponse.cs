namespace Contracts.Authentication
{
    public record LoginResponse(
        string Token,
        int ExpiryMinutes);
}