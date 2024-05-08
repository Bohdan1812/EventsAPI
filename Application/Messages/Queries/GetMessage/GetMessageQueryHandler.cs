using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ChatAggregate.Entities;
using Domain.MessageAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Messages.Queries.GetMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<GetMessageQuery, ErrorOr<Message>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IParticipationRepository _participationRepository;
        private readonly IMessageRepository _messageRepository;

        public UpdateMessageCommandHandler(IParticipationRepository participationRepository, IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _participationRepository = participationRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Message>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var messageId = MessageId.Create(request.MessageId);
            var message = await _messageRepository.GetMessage(messageId);

            if (message is null)
                return MessageError.MessageNotFound;

            var participation = await _participationRepository.GetParticipation(message.AuthorId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            return message;
        }
    }
}
