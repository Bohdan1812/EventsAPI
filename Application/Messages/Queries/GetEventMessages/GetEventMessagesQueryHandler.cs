using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ChatAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Messages.Queries.GetEventMessages
{
    public class GetEventMessagesQueryHandler : IRequestHandler<GetEventMessagesQuery, ErrorOr<List<Message>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMessageRepository _messageRepository;

        public GetEventMessagesQueryHandler(IUserRepository userRepository, IEventRepository eventRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _messageRepository = messageRepository;
        }

        public async Task<ErrorOr<List<Message>>> Handle(GetEventMessagesQuery request, CancellationToken cancellationToken)
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

            return await _messageRepository.GetEventMessages(eventId);
        }
    }
}
