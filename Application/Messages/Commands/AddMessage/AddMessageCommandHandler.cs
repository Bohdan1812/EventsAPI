using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ChatAggregate.Entities;
using Domain.ParticipationAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Messages.Commands.AddMessage
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IParticipationRepository _participationRepository;
        private readonly IMessageRepository _messageRepository;

        public AddMessageCommandHandler(IParticipationRepository participationRepository, IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _participationRepository = participationRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var participationId = ParticipationId.Create(request.ParticipationId);
            var participation = await _participationRepository.GetParticipation(participationId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            if (participation.UserId != user.Id)
                return ParticipationError.ParticipationNoPermission;

            Message? message;

            try
            {
                message = new Message(participation, request.Text);
            }
            catch (Exception ex)
            {
                return MessageError.MessageNotInitialized(ex.Message);
            }

            await _messageRepository.AddMessage(message);

            var messageId = message.Id;

            message = await _messageRepository.GetMessage(messageId);

            if (message is not null &&
                message.AuthorId == participationId &&
                message.Text == request.Text)
                return "Message created successfully!";

            return MessageError.MessageNotAdded;
        }
    }
}
