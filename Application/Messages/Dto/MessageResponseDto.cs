using Application.Users.Dto;

namespace Application.Messages.Dto
{
    public record MessageResponseDto(
        Guid Id,
        string Text,
        UserInfo Author,
        DateTime CreatedDateTime,
        DateTime UpdatedDateTime);
}
