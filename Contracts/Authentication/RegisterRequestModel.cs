namespace Contracts.Authentication
{
    public record RegisterRequestModel(
        string FirstName,
        string LastName,
        string Email,
        string Password);
}
