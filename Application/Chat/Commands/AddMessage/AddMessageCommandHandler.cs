using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.ChatAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Chat.Commands.AddMessage
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, ErrorOr<string>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IParticipationRepository _participationRepository;

        public AddMessageCommandHandler(IChatRepository chatRepository, IParticipationRepository participationRepository)
        {
            _chatRepository = chatRepository;
            _participationRepository = participationRepository;
        }

        public async Task<ErrorOr<string>> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var eventId = EventId.Create(request.EventId);

            var participation = await _participationRepository.GetParticipation(request.ApplicationUserId, eventId);

            if (participation is null)
                return ParticipationError.ParticipationNotFound;

            var chat = await _chatRepository.GetChat(eventId);


            if (chat is null)
                return Error.NotFound();

            Message? message = null;

            try 
            {
                message = new Message(participation.Id, request.Text);
            }
            catch(Exception ex)
            {
                return ChatError.MessageNotInitialized(ex.Message);
            }

            try 
            {
                chat.AddMessage(message);
            }
            catch(Exception ex)  
            {
                return ChatError.MessageNotAddedToChat(ex.Message);
            }

            chat = await _chatRepository.GetChat(eventId);

            if (chat is not null)
            {
                var createdMessage = chat.Messages.FirstOrDefault(m => m.Id == message.Id);
                if (createdMessage is not null)
                    return "Message created successfully!";
            }

            return Error.Failure();
        }
    }
}
