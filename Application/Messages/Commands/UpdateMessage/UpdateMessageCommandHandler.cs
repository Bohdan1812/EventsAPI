﻿using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.MessageAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, ErrorOr<string>>
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

        public async Task<ErrorOr<string>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
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

            if (participation.UserId != user.Id)
                return MessageError.MessageNoPermission;

            try
            {
                message.UpdateText(participation.Id, request.Text);
            }
            catch (Exception ex)
            {
                return MessageError.MessageNotUpdated(ex.Message);
            }

            await _messageRepository.UpdateMessage(message);

            message = await _messageRepository.GetMessage(messageId);

            if (message is not null &&
                message.AuthorId == participation.Id &&
                message.Text == request.Text)
                return "Message updated successfully!";

            return MessageError.MessageNotUpdatedInDb;
        }
    }
}
