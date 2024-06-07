using Application.Common.Errors;
using Application.Messages.Dto;
using Application.Persistence.Repositories;
using Application.Users.Dto;
using Domain.ChatAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Messages.Queries.GetEventMessages
{
    public class GetEventMessagesQueryHandler : IRequestHandler<GetEventMessagesQuery, ErrorOr<List<MessageResponseDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IParticipationRepository _participationRepository;

        public GetEventMessagesQueryHandler(IUserRepository userRepository, IEventRepository eventRepository, IMessageRepository messageRepository, IParticipationRepository participationRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _messageRepository = messageRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<List<MessageResponseDto>>> Handle(GetEventMessagesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            var eventId = EventId.Create(request.EventId);

            var @event = await _eventRepository.GetFullEvent(eventId);

            if (@event is null)
                return EventError.EventNotFound;

            if (@event.Participations.FirstOrDefault(p => p.UserId == user.Id) is null)
                return MessageError.MessageNoPermission;

            var messages =  await _messageRepository.GetEventMessages(eventId);

            List<MessageResponseDto> messageResponses = new List<MessageResponseDto>();
            foreach(var message in messages) 
            {
                var participation = await _participationRepository.GetParticipation(message.AuthorId);

                if (participation is null)
                    return ParticipationError.ParticipationNotFound;

                var author = await _userRepository.GetFullUser(participation.UserId);

                if (author is null)
                    return UserError.UserNotFound;

                messageResponses.Add(new MessageResponseDto(
                    message.Id.Value,
                    message.Text,
                    new UserInfo(
                        author.Id.Value,
                        author.FirstName,
                        author.LastName,
                        author.ApplicationUser.Email,
                        author.PhotoPath),
                    message.CreatedDateTime,
                    message.UpdatedDateTime));
            }

            return messageResponses;
        }
    }
}
