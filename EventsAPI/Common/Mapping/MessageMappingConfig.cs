using Application.Messages.Commands.AddMessage;
using Application.Messages.Commands.DeleteMessage;
using Application.Messages.Commands.UpdateMessage;
using Application.Messages.Queries.GetEventMessages;
using Application.Messages.Queries.GetMessage;
using Contracts.Event;
using Contracts.Message;
using Domain.ChatAggregate.Entities;
using Mapster;

namespace Api.Common.Mapping
{
    public class MessageMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid appUserId, AddMessageRequestModel request), AddMessageCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.ParticipationId, src => src.request.ParticipationId)
                .Map(dest => dest.Text, src => src.request.Text);

            config.NewConfig<(Guid appUserId, UpdateMessageRequestModel request), UpdateMessageCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.MessageId, src => src.request.MessageId)
                .Map(dest => dest.Text, src => src.request.Text);

            config.NewConfig<(Guid appUserId, DeleteMessageRequestModel request), DeleteMessageCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.MessageId, src => src.request.MessageId);

            config.NewConfig<(Guid appUserId, GetEventMessagesRequestModel request), GetEventMessagesQuery>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId);

            config.NewConfig<(Guid appUserId, GetMessageRequestModel request), GetMessageQuery>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.MessageId, src => src.request.MessageId);

            config.NewConfig<Message, MessageResponse>()
                .Map(dest => dest.AuthorId, src => src.AuthorId.Value)
                .Map(dest => dest.Text, src => src.Text)
                .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime)
                .Map(dest => dest.UpdatedDateTime, src => src.UpdatedDateTime);
        }
    }
}
