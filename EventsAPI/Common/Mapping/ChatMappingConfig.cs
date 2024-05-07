using Application.Authentication.Commands.Register;
using Application.Chat.Commands.AddMessage;
using Application.Users.Commands.Delete;
using Contracts.Authentication;
using Contracts.Chat;
using Mapster;

namespace Api.Common.Mapping
{
    public class ChatMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid ApplicationUserId, AddMessageRequestModel request), AddMessageCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.ApplicationUserId)
                .Map(dest => dest.EventId, src => src.request.EventId)
                .Map(dest => dest.Text, src => src.request.Text);
        }
    }
}
