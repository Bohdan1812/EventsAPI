namespace Contracts.Authentication
{
    public record ChangePasswordRequestModel(string OldPassword, string NewPassword);
}
